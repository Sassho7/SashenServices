using Microsoft.AspNetCore.Mvc;
using SmartGarage.Services;
using SmartGarage.Models.Enums;


namespace SmartGarage.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
