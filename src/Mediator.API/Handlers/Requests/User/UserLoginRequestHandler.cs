using Mediator.API.Model;
using Mediator.API.Model.Records.Requests;
using Mediator.API.Model.Records.Requests.User;
using Mediator.API.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mediator.API.Handlers.Requests;

public class UserLoginRequestHandler(
    LinkDbContext dbContext,
    IConfiguration configuration,
    IPasswordHasher<User> passwordHasher,
    JwtService jwtService)
    : IRequestHandler<UserLoginRequest, string>
{
    public async Task<string> Handle(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);
        if (user == null || passwordHasher.VerifyHashedPassword(user, user.Password, request.PasswordHash) != PasswordVerificationResult.Success)
        {
            throw new Exception("Authentication failed."); 
        }

        return jwtService.GenerateJwtToken(user, configuration);
    }
    
}