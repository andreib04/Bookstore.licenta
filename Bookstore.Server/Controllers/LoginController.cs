using Bookstore.Server.Data.Models;
using Bookstore.Server.Services;
using Microsoft.AspNetCore.Mvc;


namespace Bookstore.Server.Controllers;

[Route("api/[controller]")]
[Controller]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        try
        {
            var token = await _userService.LoginUser(userLogin.Email, userLogin.Password);
            return Ok(token);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("User not found");
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid credentials");
        }
    }
}