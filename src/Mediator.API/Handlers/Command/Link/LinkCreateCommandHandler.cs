using Mediator.API.Model;
using Mediator.API.Model.Records.Commands.Link;
using Mediator.API.Model.Records.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mediator.API.Handlers.Command.Link;

public class LinkCreateCommandHandler(LinkDbContext dbContext) : IRequestHandler<LinkCreateCommand, string>
{
    
    public async Task<string> Handle(LinkCreateCommand request, CancellationToken cancellationToken)
    {
        var shortenedUrl = GenerateShortenedUrl(request.Link);
    
        var newLink = new Model.Link
        {
            OriginalUrl = request.Link,
            ShorteredUrl = shortenedUrl,
            UserId = request.UserId,
        };
    
        dbContext.Links.Add(newLink);
        await dbContext.SaveChangesAsync(cancellationToken);
        return shortenedUrl;
    }

    private static string GenerateShortenedUrl(string originalUrl)
    {
        var urlHash = originalUrl.GetHashCode().ToString("X"); 
        var shortenedUrl = $"http://localhost:5000/{urlHash}";
    
        return shortenedUrl;
    }
}