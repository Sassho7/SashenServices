using Microsoft.AspNetCore.Mvc;

namespace SmartGarage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
