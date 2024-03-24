using Mediator.API.Model;
using Mediator.API.Model.Records.Commands.User;
using Mediator.API.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mediator.API.Handlers.Command;

public class UserRegisterCommandHandler(
    LinkDbContext dbContext,
    IPasswordHasher<User> passwordHasher,
    IConfiguration configuration, JwtService jwtService)
    : IRequestHandler<UserRegisterCommand, string>
{
    public async Task<string> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        if (await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken: cancellationToken) != null)
        {
            throw new Exception("User already exists.");
        }

        var user = new User { Email = request.Email };
        user.Password = passwordHasher.HashPassword(user, request.Password);
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return jwtService.GenerateJwtToken(user, configuration); 
    }
}