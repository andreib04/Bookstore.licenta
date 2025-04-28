using System.Security.Claims;
using Bookstore.Services.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Bookstore.Server.Configurations;

public static class AuthorizationConfiguration
{
    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            var admin = UserRolesConstants.Admin.ToString();
            var member = UserRolesConstants.Member.ToString();

            var defaultAuthorizationPolicyBuilder =
                new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            
            defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
            
            options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            options.AddPolicy(PoliciesConstants.ADMIN, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, admin);
            });
            
            options.AddPolicy(PoliciesConstants.MEMBER, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, member);
            });
        });
        
        return services;
    }
}