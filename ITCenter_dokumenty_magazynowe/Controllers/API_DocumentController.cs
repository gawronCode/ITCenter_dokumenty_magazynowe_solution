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

namespace ITCenter_dokumenty_magazynowe.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class API_DocumentController : Controller
    {
        private readonly IWarehouseDocRepo _warehouseDocRepo;
        private readonly IOperationLogRepo _operationLogRepo;
        private readonly IPositionRepo _positionRepo;
        public API_DocumentController(IOperationLogRepo operationLogRepo,
            IWarehouseDocRepo warehouseDocRepo,
            IPositionRepo positionRepo)
        {
            _operationLogRepo = operationLogRepo;
            _warehouseDocRepo = warehouseDocRepo;
            _positionRepo = positionRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var warehousedocs = _warehouseDocRepo.GetAll().Select(i => new {
                i.Id,
                i.Name,
                i.ClientNumber,
                i.Date,
                i.NetPrice,
                i.GrossPrice
            });
            
            return Json(await DataSourceLoader.LoadAsync(warehousedocs, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new WarehouseDoc();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = await _warehouseDocRepo.Create(model);
            await LogDocData(model, "create");

            return Json(new { result.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _warehouseDocRepo.GetById(key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _warehouseDocRepo.Save();
            await LogDocData(model, "update");

            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key)
        {
            var model = await _warehouseDocRepo.GetById(key);
            var modelChildren = await _positionRepo.GetAllByParentId(model.Id);
            foreach (var child in modelChildren)
            {
                await LogPositionData(child, "delete");
            }
            await _warehouseDocRepo.Delete(model);
            await LogDocData(model, "delete");
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
        private async Task LogPositionData(Models.DbModels.Position model, string operation)
        {
            await _operationLogRepo.Create(new OperationLog
            {
                Date = DateTime.Now,
                Info = $"{operation};Position;{model.Id};{model.ProductName};{model.Quantity};{model.NetPrice};{model.GrossPrice};{model.WarehouseDocId}",
                ObjectId = model.Id,
            });
        }


        private void PopulateModel(WarehouseDoc model, IDictionary values) {
            var ID = nameof(WarehouseDoc.Id);
            var NAME = nameof(WarehouseDoc.Name);
            var CLIENT_NUMBER = nameof(WarehouseDoc.ClientNumber);
            var DATE = nameof(WarehouseDoc.Date);
            var NET_PRICE = nameof(WarehouseDoc.NetPrice);
            var GROSS_PRICE = nameof(WarehouseDoc.GrossPrice);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

            if(values.Contains(CLIENT_NUMBER)) {
                model.ClientNumber = Convert.ToInt32(values[CLIENT_NUMBER]);
            }

            if(values.Contains(DATE)) {
                model.Date = values[DATE] != null ? Convert.ToDateTime(values[DATE]) : (DateTime?)null;
            }

            if(values.Contains(NET_PRICE)) {
                model.NetPrice = Convert.ToInt32(values[NET_PRICE]);
            }

            if(values.Contains(GROSS_PRICE)) {
                model.GrossPrice = Convert.ToInt32(values[GROSS_PRICE]);
            }
        }

        private static string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = (from entry 
                in modelState from 
                error in 
                entry.Value.Errors 
                select error.ErrorMessage).ToList();

            return string.Join(" ", messages);
        }
    }
}