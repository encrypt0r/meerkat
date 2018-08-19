using Meerkat.Web.Data;
using Meerkat.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Meerkat.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStatisticsService _eventStatistics;

        public HomeController(IUnitOfWork unitOfWork, IEventStatisticsService eventStatistics)
        {
            _unitOfWork = unitOfWork;
            _eventStatistics = eventStatistics;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEventGroups()
        {
            var summaries = await _eventStatistics.GetEventGroupSummariesAsync();

            return Json(summaries);
        }
    }
}
