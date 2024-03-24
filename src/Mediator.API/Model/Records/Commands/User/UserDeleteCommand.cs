using System.Text.Json.Serialization;
using MediatR;

namespace Mediator.API.Model.Records.Commands.User;

public record UserDeleteCommand([property: JsonPropertyName("email")] string Email) : IRequest<string>;
