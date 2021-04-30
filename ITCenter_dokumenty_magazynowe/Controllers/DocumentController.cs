using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ITCenter_dokumenty_magazynowe.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
