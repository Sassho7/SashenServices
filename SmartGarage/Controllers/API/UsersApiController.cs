using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGarage.Exceptions;
using SmartGarage.Models;
using SmartGarage.Models.DTOs;
using SmartGarage.Models.QueryParameters;
using SmartGarage.Services;
using System.Security.Claims;
using SmartGarage.DTOs;
using SmartGarage.ViewModels;
using System.Threading.Tasks;

namespace SmartGarage.Controllers.API
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        public UsersApiController(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> SignUp([FromBody] UserRegistrationDTO userRequestDTO)
        {
            try
            {
                var newUser = userService.Create(userRequestDTO);
                // Assuming the login page route is named "Login" in your routing configuration
                return RedirectToAction("Login", "Account");
            }
            catch (DuplicateEntityExcetion e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("")]
        public IActionResult GetAll([FromQuery] UserQueryParameters filterParameters)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var users = userService.FilterBy(filterParameters);
                return Ok(users);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var user = userService.GetById(int.Parse(id));
                return Ok(user);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDTO requestDTO)
        {
            try
            {
                var token = userService.Login(requestDTO);
                return Ok(token);
            }
            catch (AuthorizationException e)
            {
                return BadRequest(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] UpdateUserRequestDTO updatedUser)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var result = userService.Update(int.Parse(id), updatedUser, username);
                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                userService.Delete(int.Parse(id), username);
                return Ok("User has been successfully deleted");
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
