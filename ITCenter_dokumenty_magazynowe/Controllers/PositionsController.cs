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

namespace ITCenter_dokumenty_magazynowe.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PositionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public PositionsController(ApplicationDbContext context) {
            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var positions = _context.Positions.Select(i => new {
                i.Id,
                i.ProductName,
                i.Quantity,
                i.NetPrice,
                i.GrossPrice,
                i.WarehouseDocId
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(positions, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Models.DbModels.Position();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Positions.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Positions.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Positions.FirstOrDefaultAsync(item => item.Id == key);

            _context.Positions.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> WarehouseDocsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.WarehouseDocs
                         orderby i.Name
                         select new {
                             Value = i.Id,
                             Text = i.Name
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Models.DbModels.Position model, IDictionary values) {
            string ID = nameof(Models.DbModels.Position.Id);
            string PRODUCT_NAME = nameof(Models.DbModels.Position.ProductName);
            string QUANTITY = nameof(Models.DbModels.Position.Quantity);
            string NET_PRICE = nameof(Models.DbModels.Position.NetPrice);
            string WAREHOUSE_DOC_ID = nameof(Models.DbModels.Position.WarehouseDocId);

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
                model.NetPrice = Convert.ToInt32(values[NET_PRICE]);
                model.GrossPrice = (model.NetPrice * 121)/100;
            }
            
            if(values.Contains(WAREHOUSE_DOC_ID)) {
                model.WarehouseDocId = Convert.ToInt32(values[WAREHOUSE_DOC_ID]);
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