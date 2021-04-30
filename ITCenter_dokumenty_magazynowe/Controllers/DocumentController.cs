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
        private readonly IOperationLogRepo _operationLogRepo;
        public DocumentController(IWarehouseDocRepo warehouseDocRepo, IOperationLogRepo operationLogRepo)
        {
            _warehouseDocRepo = warehouseDocRepo;
            _operationLogRepo = operationLogRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        

    }
}
