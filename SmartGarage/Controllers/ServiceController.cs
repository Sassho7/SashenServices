using Microsoft.AspNetCore.Mvc;

namespace SmartGarage.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
