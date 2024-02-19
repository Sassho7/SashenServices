using Microsoft.AspNetCore.Mvc;

namespace SmartGarage.Controllers.API
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
