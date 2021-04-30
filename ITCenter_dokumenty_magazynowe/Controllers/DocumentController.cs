using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Repositories.IRepos;
using Microsoft.AspNetCore.Authorization;

namespace ITCenter_dokumenty_magazynowe.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {

        private readonly IWarehouseDocRepo _warehouseDocRepo;
        public DocumentController(IWarehouseDocRepo warehouseDocRepo)
        {
            _warehouseDocRepo = warehouseDocRepo;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }


    }
}
