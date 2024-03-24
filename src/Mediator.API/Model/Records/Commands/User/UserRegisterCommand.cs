using System.Text.Json.Serialization;
using MediatR;

namespace Mediator.API.Model.Records.Commands.User;

public record UserRegisterCommand(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")]
    string Password) : IRequest<string>;