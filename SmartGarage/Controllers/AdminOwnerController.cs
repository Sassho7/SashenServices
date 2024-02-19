using Microsoft.AspNetCore.Mvc;

namespace SmartGarage.Controllers
{
    public class AdminOwnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
