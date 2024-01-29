using Microsoft.AspNetCore.Mvc;

namespace SmartGarage.Controllers
{
    public class UserControler : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
