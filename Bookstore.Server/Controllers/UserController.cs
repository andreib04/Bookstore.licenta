using System.Security.Claims;
using Bookstore.Server.Data.Models;
using Bookstore.Server.Services;
using Bookstore.Server.Validations;
using Bookstore.Services.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Bookstore.Server.Controllers;

[Controller]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
   private readonly IUserService _userService;

   public UserController(IUserService userService)
   {
      _userService = userService;
   }

   [HttpGet]
   [AllowAnonymous]
   private User GetCurrentUser()
   {
      var identity = HttpContext.User.Identity as ClaimsIdentity;

      if (identity != null)
      {
         var userClaims = identity.Claims;

         return new User
         {
            Id = int.Parse(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value),
            FirstName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
            Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
            Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
         };
      }

      return null;
   }

   [HttpGet]
   [AllowAnonymous]
   public async Task<IActionResult> GetAllUsers()
   {
      try
      {
         var users = await _userService.GetAllUsers();
         return Ok(users);
      }
      catch
      {
         return new ObjectResult("Something went wrong")
         {
            StatusCode = 500
         };
      }
   }

   [HttpGet("check-email")]
   public async Task<IActionResult> CheckEmail(string email)
   {
      try
      {
         var exists = await _userService.EmailExistsAsync(email);
         return Ok(new {exists});
      }
      catch (Exception ex)
      {
         throw new Exception(ex.Message);
      }
   }

   [HttpGet("{id}")]
   [AllowAnonymous]
   public async Task<IActionResult> GetUserById(int id)
   {
      
      try
      {
         var user = await _userService.GetUserById(id.ToString());
         return Ok(user);
      }
      catch
      {
         return new ObjectResult("Something went wrong")
         {
            StatusCode = 500
         };
      }
   }

   [HttpPost]
   [AllowAnonymous]
   public async Task<IActionResult> AddUser([FromBody] User user)
   {
      try
      { 
         await _userService.AddUser(user);
         return Ok();
      }
      catch (KeyNotFoundException e)
      {
         return new NotFoundObjectResult(e.Message);
      }
   }

   [HttpPut("{id}")]
   [Authorize(Policy = UserRolesConstants.Admin)]
   public async Task<IActionResult> EditUser(int id, [FromBody] User user)
   {
      if (id != user.Id)
         return BadRequest();

      try
      {
         await _userService.EditUser(user);
         return NoContent();
      }
      catch (Exception e)
      { 
         return new NotFoundObjectResult(e.Message);
      }
   }

   [HttpDelete("{id}")]
   [Authorize(Policy = UserRolesConstants.Admin)]
   public async Task<IActionResult> DeleteUser(int id)
   {
      try
      {
         await _userService.DeleteUser(id);
         return NoContent();
      }
      catch
      {
         return new ObjectResult("Something went wrong")
         {
            StatusCode = 500
         };
      }
   }
}