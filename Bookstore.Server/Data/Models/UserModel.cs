using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace Bookstore.Server.Data.Models;

public class UserModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    [SwaggerSchema("User role, defaults to 'Member'")]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    [DefaultValue("Member")]
    public string Role { get; set; } = "Member";
}