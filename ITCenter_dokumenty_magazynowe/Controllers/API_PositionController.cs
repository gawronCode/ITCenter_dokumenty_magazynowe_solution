using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Models.DbModels;
using ITCenter_dokumenty_magazynowe.Repositories.IRepos;
using Microsoft.AspNetCore.Authorization;
using Position = ITCenter_dokumenty_magazynowe.Models.DbModels.Position;

namespace ITCenter_dokumenty_magazynowe.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class API_PositionController : Controller
    {

        private readonly IPositionRepo _positionRepo;
        private readonly IWarehouseDocRepo _warehouseDocRepo;
        private readonly IOperationLogRepo _operationLogRepo;
        
        public API_PositionController(IPositionRepo positionRepo,
            IOperationLogRepo operationLogRepo,
            IWarehouseDocRepo warehouseDocRepo) {
            _operationLogRepo = operationLogRepo;
            _warehouseDocRepo = warehouseDocRepo;
            _positionRepo = positionRepo;
        }
        

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var positions = _positionRepo.GetAll().Select(i => new {
                i.Id,
                i.ProductName,
                i.Quantity,
                i.NetPrice,
                i.GrossPrice,
                i.WarehouseDocId
            });
            
            return Json(await DataSourceLoader.LoadAsync(positions, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Position();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = await _positionRepo.Create(model);
            await LogPositionData(model, "create");
            await UpdateParent(model);

            return Json(new { result.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _positionRepo.GetById(key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await LogPositionData(model, "update");
            await UpdateParent(model);

            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key)
        {
            var model = await _positionRepo.GetById(key);
            await LogPositionData(model, "delete");
            await _positionRepo.Delete(model);
            await UpdateParent(model);
        }

        [NonAction]
        private async Task UpdateParent(Position model)
        {
            var parentId = model.WarehouseDocId;
            var parentModel = await _warehouseDocRepo.GetById(parentId);
            var parentPositions = await _positionRepo.GetAllByParentId(parentId);
            var newNetPrice = parentPositions.Sum(q => q.NetPrice * q.Quantity);
            var newGrossPrice = parentPositions.Sum(q => q.GrossPrice * q.Quantity);
            parentModel.NetPrice = newNetPrice;
            parentModel.GrossPrice = newGrossPrice;
            await LogDocData(parentModel, "update");
        }

        [NonAction]
        private async Task LogDocData(WarehouseDoc model, string operation)
        {
            await _operationLogRepo.Create(new OperationLog
            {
                Date = DateTime.Now,
                Info = $"{operation};Doc;{model.Id};{model.Name};{model.Date};{model.ClientNumber};{model.NetPrice};{model.GrossPrice}",
                ObjectId = model.Id,
            });
        }

        [NonAction]
        private async Task LogPositionData(Position model, string operation)
        {
            await _operationLogRepo.Create(new OperationLog
            {
                Date = DateTime.Now,
                Info = $"{operation};Position;{model.Id};{model.ProductName};{model.Quantity};{model.NetPrice};{model.GrossPrice};{model.WarehouseDocId}",
                ObjectId = model.Id,
            });
        }

        [HttpGet]
        public async Task<IActionResult> WarehouseDocsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _warehouseDocRepo.GetAll()
                         orderby i.Name
                         select new {
                             Value = i.Id,
                             Text = i.Name
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private static void PopulateModel(Position model, IDictionary values) {
            var ID = nameof(Position.Id);
            var PRODUCT_NAME = nameof(Position.ProductName);
            var QUANTITY = nameof(Position.Quantity);
            var NET_PRICE = nameof(Position.NetPrice);
            var WAREHOUSE_DOC_ID = nameof(Position.WarehouseDocId);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(PRODUCT_NAME)) {
                model.ProductName = Convert.ToString(values[PRODUCT_NAME]);
            }

            if(values.Contains(QUANTITY)) {
                model.Quantity = Convert.ToInt32(values[QUANTITY]);
            }

            if(values.Contains(NET_PRICE)) {
                model.NetPrice = Convert.ToDouble(values[NET_PRICE]);
                model.NetPrice = Math.Round(model.NetPrice, 2);
                model.GrossPrice = model.NetPrice * 1.21;
                model.GrossPrice = Math.Round(model.GrossPrice, 2);
            }
            
            if(values.Contains(WAREHOUSE_DOC_ID)) {
                model.WarehouseDocId = Convert.ToInt32(values[WAREHOUSE_DOC_ID]);
            }
        }

        private static string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = (from entry 
                in modelState 
                from error in entry.Value.Errors 
                select error.ErrorMessage).ToList();

            return string.Join(" ", messages);
        }
    }
}