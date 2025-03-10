using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bookstore.Server.Data.Models;
using Bookstore.Server.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Bookstore.Server.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
        _configuration = configuration;
    }

    public async Task<string> LoginUser(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if(user == null)
            throw new KeyNotFoundException("User not found");
        
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        if(result != PasswordVerificationResult.Success)
            throw new UnauthorizedAccessException("Invalid password");

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("first_name", user.FirstName),
            new Claim("last_name", user.LastName),
            new Claim("email", user.Email),
            new Claim("role", user.Role),
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var list = await _userRepository.GetAllUsers();
        return list;
    }

    public async Task<User?> GetUserById(string id)
    {
        var user = await _userRepository.GetUserById(id);
        return user;
    }

    public async Task AddUser(User user)
    {
        user.Password = _passwordHasher.HashPassword(user, user.Password);
        await _userRepository.AddUser(user);
    }

    public async Task EditUser(User user)
    {
        var dbUser = await _userRepository.GetUserById(user.Id.ToString());
        
        dbUser.FirstName = user.FirstName;
        dbUser.LastName = user.LastName;
        dbUser.Email = user.Email;
        dbUser.Password = user.Password;
        dbUser.Role = user.Role;
        
        await _userRepository.EditUser(dbUser);
    }

    public async Task DeleteUser(int id)
    {
        await _userRepository.DeleteUser(id);
    }
}