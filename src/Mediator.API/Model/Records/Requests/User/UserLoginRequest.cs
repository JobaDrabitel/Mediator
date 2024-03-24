using System.Text.Json.Serialization;
using MediatR;

namespace Mediator.API.Model.Records.Requests.User;

public record UserLoginRequest(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")]
    string PasswordHash) : IRequest<string>;