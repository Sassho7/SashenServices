using Microsoft.AspNetCore.Mvc;
using SmartGarage.DTOs;
using SmartGarage.Exceptions;
using SmartGarage.Models.DTOs;
using SmartGarage.Services;
using SmartGarage.ViewModels;

namespace SmartGarage.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginDto = new UserLoginDTO
            {
                Username = model.Username,
                Password = model.Password
            };

            try
            {
                var token = _userService.Login(loginDto);
                HttpContext.Response.Cookies.Append("userToken", token);

                return RedirectToAction("Index", "Home");
            }
            catch (AuthorizationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var registrationDTO = new UserRegistrationDTO
            {
                Username = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password
            };

            try
            {
                _userService.Create(registrationDTO);
                return RedirectToAction("Login");
            }
            catch (DuplicateEntityExcetion ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(model);
            }
        }
    }
}
