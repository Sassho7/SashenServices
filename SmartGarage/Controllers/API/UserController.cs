using Microsoft.AspNetCore.Mvc;

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
            var newUser = _userService.RegisterUser(request);
            return Ok(new { Message = "Registration successful", UserId = newUser.Id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Registration failed", Error = ex.Message });
        }
    }
}
