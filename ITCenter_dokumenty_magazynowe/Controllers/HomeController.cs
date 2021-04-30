using ITCenter_dokumenty_magazynowe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace ITCenter_dokumenty_magazynowe.Controllers
{
    public class HomeController : Controller
    {

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
