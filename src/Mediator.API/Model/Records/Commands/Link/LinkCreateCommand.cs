using System.Text.Json.Serialization;
using MediatR;

namespace Mediator.API.Model.Records.Commands.Link;

public record LinkCreateCommand
    : IRequest<string>
{
    public LinkCreateCommand(string link, int userId = 0)
    {
        Link = link;
        UserId = userId;
    }

    public string Link { get; init; }
    public int UserId { get; init; }
}