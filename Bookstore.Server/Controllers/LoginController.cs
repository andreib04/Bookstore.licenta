using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bookstore.Server.Data;
using Bookstore.Server.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Bookstore.Server.Controllers;

[Route("api/[controller]")]
[Controller]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly DatabaseContext _dbContext;
    
    public LoginController(IConfiguration configuration, DatabaseContext dbContext)
    {
      _configuration = configuration;
      _dbContext = dbContext;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        var user = Authenticate(userLogin);

        if (user != null)
        {
            var token = Generate(user);
            return Ok(token);
        }

        return NotFound("User not found");
    }

    private User Authenticate(UserLogin userLogin)
    {
        var currentUser = _dbContext.Users.FirstOrDefault(u => u.Email.ToLower() == userLogin.Email.ToLower() && u.Password == userLogin.Password);
        
        if(currentUser == null)
            throw new KeyNotFoundException("Email or password is incorrect!");

        return currentUser;
    }
    
    private string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}