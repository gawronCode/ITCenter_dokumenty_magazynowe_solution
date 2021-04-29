using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCenter_dokumenty_magazynowe.Controllers
{
    public class GitHubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
