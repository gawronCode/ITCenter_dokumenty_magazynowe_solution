using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Data;
using ITCenter_dokumenty_magazynowe.Models.DbModels;
using ITCenter_dokumenty_magazynowe.Repositories.IRepos;
using Microsoft.AspNetCore.Authorization;

namespace ITCenter_dokumenty_magazynowe.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class WarehouseDocsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPositionRepo _positionRepo;
        private readonly IWarehouseDocRepo _warehouseDocRepo;
        private readonly IOperationLogRepo _operationLogRepo;
        public WarehouseDocsController(ApplicationDbContext context,
            IPositionRepo positionRepo,
            IOperationLogRepo operationLogRepo,
            IWarehouseDocRepo warehouseDocRepo)
        {
            _context = context;
            _operationLogRepo = operationLogRepo;
            _warehouseDocRepo = warehouseDocRepo;
            _positionRepo = positionRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var warehousedocs = _context.WarehouseDocs.Select(i => new {
                i.Id,
                i.Name,
                i.ClientNumber,
                i.Date,
                i.NetPrice,
                i.GrossPrice
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(warehousedocs, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new WarehouseDoc();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.WarehouseDocs.Add(model);
            await _context.SaveChangesAsync();

            await _operationLogRepo.Create(new OperationLog
            {
                Date = model.Date,
                Info = $"create;Doc;{model.Id};{model.Name};{model.ClientNumber};{model.NetPrice};{model.GrossPrice}",
                ObjectId = model.Id,
            });

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.WarehouseDocs.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();

            await _operationLogRepo.Create(new OperationLog
            {
                Date = model.Date,
                Info = $"update;Doc;{model.Id};{model.Name};{model.ClientNumber};{model.NetPrice};{model.GrossPrice}",
                ObjectId = model.Id,
            });

            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.WarehouseDocs.FirstOrDefaultAsync(item => item.Id == key);
            _context.WarehouseDocs.Remove(model);
            await _operationLogRepo.Create(new OperationLog
            {
                Date = model.Date,
                Info = $"delete;Doc;{model.Id};{model.Name};{model.ClientNumber};{model.NetPrice};{model.GrossPrice}",
                ObjectId = model.Id,
            });
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(WarehouseDoc model, IDictionary values) {
            string ID = nameof(WarehouseDoc.Id);
            string NAME = nameof(WarehouseDoc.Name);
            string CLIENT_NUMBER = nameof(WarehouseDoc.ClientNumber);
            string DATE = nameof(WarehouseDoc.Date);
            string NET_PRICE = nameof(WarehouseDoc.NetPrice);
            string GROSS_PRICE = nameof(WarehouseDoc.GrossPrice);

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

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}