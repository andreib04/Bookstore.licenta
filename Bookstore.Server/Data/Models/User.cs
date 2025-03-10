using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using Bookstore.Server.Validations;

namespace Bookstore.Server.Data.Models;

public class User
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