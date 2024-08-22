using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Infrastructure.Authentication;

namespace TaskManagement.Api.Extensions;

public static class ApiExtensions
{
    public static void AddApiAuthentication(
        this IServiceCollection services,
        JwtOptions jwtOptions)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                var signingKeyBytes = Encoding.UTF8
                    .GetBytes(jwtOptions.SecretKey);

                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });
    }
}