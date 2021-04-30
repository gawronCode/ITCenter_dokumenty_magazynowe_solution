using ITCenter_dokumenty_magazynowe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Repositories.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ITCenter_dokumenty_magazynowe.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWarehouseDocRepo _warehouseDocRepo;
        public HomeController(IWarehouseDocRepo warehouseDocRepo)
        {
            _warehouseDocRepo = warehouseDocRepo;

        }
        
        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Document");
            }

            return View();

        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
