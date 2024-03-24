using Mediator.API.Model;
using Mediator.API.Model.Records.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mediator.API.Handlers.Command;

public class UserDeleteCommandHandler(LinkDbContext dbContext) : IRequestHandler<UserDeleteCommand, string>
{
    public async Task<string> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email,
                cancellationToken: cancellationToken);
            if (user is null) return "NotFound";
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync(cancellationToken);
            return "Ok";
        }
        catch(Exception ex)
        {
            return ex.Message;
        }
    }
}