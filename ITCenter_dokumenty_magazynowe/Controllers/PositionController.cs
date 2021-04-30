using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ITCenter_dokumenty_magazynowe.Controllers
{
    [Authorize]
    public class PositionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
