using Microsoft.AspNetCore.Mvc;
using SmartGarage.Models;
using SmartGarage.Services;
using System;

namespace SmartGarage.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistrationRequest request)
        {
            try
            {
                var registrationSuccess = _userService.RegisterAsync(request.Email, request.Password, request.PhoneNumber).Result;
                if (registrationSuccess)
                {
                    return Ok(new { Message = "Registration successful" });
                }
                else
                {
                    return BadRequest(new { Message = "Registration failed" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Registration failed", Error = ex.Message });
            }
        }
    }
}
