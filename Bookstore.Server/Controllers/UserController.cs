using System.Security.Claims;
using Bookstore.Server.Data.Models;
using Bookstore.Server.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Bookstore.Server.Controllers;

[Controller]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
   private readonly IUserService _userService;
   private readonly AbstractValidator<User> _userValidator;

   public UserController(IUserService userService, AbstractValidator<User> userValidator)
   {
      _userService = userService;
      _userValidator = userValidator;
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
         var validation = _userValidator.Validate(user);

         if (!validation.IsValid)
         {
            return new BadRequestObjectResult(validation.Errors.Select(error => error.ErrorMessage));
         }
         
         await _userService.AddUser(user);
         return Ok();
      }
      catch (KeyNotFoundException e)
      {
         return new NotFoundObjectResult(e.Message);
      }
   }

   [HttpPut("{id}")]
   [Authorize(Policy = "Admin")]
   public async Task<IActionResult> EditUser(int id, [FromBody] User user)
   {
      if (id != user.Id)
         return BadRequest();

      try
      {
         var validation = _userValidator.Validate(user);

         if (validation.IsValid)
         {
            return new BadRequestObjectResult(validation.Errors.Select(error => error.ErrorMessage));
         }
         
         await _userService.EditUser(user);
         return NoContent();
      }
      catch (KeyNotFoundException e)
      { 
         return new NotFoundObjectResult(e.Message);
      }
   }

   [HttpDelete("{id}")]
   [Authorize(Policy = "Admin")]
   public async Task<IActionResult> DeleteUser(int id)
   {
      try
      {
         await _userService.DeleteUser(id.ToString());
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