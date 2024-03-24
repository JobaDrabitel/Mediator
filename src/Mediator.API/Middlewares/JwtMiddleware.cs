using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Mediator.API.Model;
using Microsoft.IdentityModel.Tokens;

namespace Mediator.API.Middlewares;

public class JwtMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, LinkDbContext dbContext, IConfiguration configuration)
    {
        var token = context.Request.Cookies["JWT-Token"];
        if (!string.IsNullOrEmpty(token))
        {
            AttachUserToContext(context, dbContext, token, configuration);
        }

        await next(context);
    }

    private static void AttachUserToContext(HttpContext context, LinkDbContext dbContext, string token, IConfiguration configuration)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]!);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var uniqueName = jwtToken.Claims.First().Value;
            var userId = int.Parse(uniqueName);

            context.Items["User"] = dbContext.Users.Find(userId);
        }
        catch
        {
            // ignored
        }
    }
}
