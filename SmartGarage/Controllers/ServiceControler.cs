using Microsoft.AspNetCore.Mvc;

namespace SmartGarage.Controllers
{
    public class ServiceControler : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
