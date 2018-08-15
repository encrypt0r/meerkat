using Meerkat.Web.Areas.Dashboard.Dtos;
using Meerkat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Meerkat.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEventGroups()
        {
            var groups = await _unitOfWork.EventGroups.GetLatestN(25);
            var hits = await _unitOfWork.EventGroups.GetHits(groups.Select(g => g.Id));
            var vms = groups.Select(g => new EventGroupDto(g, hits));

            return Json(vms);
        }
    }
}
