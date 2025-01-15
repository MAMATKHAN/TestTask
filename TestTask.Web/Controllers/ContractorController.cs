using Microsoft.AspNetCore.Mvc;
using TestTask.BLL.Services.Interfaces;
using TestTask.Web.Models.Contractors;

namespace TestTask.Web.Controllers
{
    public class ContractorController : Controller
    {
        private readonly IContractorService _contractorService;
        
        public ContractorController(IContractorService contractorService)
        {
            _contractorService = contractorService;
        }

        public async Task<IActionResult> Index(CancellationToken token)
        {
            var contractors = await _contractorService.GetAll(token);
            return View(new IndexViewModel { Contractors = contractors });
        }
    }
}
