using Microsoft.AspNetCore.Mvc;

namespace SmartGarage.Controllers
{
    public class AdminOwnerControler : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
