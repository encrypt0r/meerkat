using Meerkat.Web.Areas.Dashboard.Models;
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
        public async Task<IActionResult> Index()
        {
            var events = await _unitOfWork.Events.GetAllAsync();

            var vms = events.Select(e => new EventViewModel(e));

            return View(vms);
        }
    }
}
