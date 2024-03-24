using Mediator.API.Extensions;
using Mediator.API.Model;
using Mediator.API.Model.Records.Commands;
using Mediator.API.Model.Records.Commands.Link;
using Mediator.API.Model.Records.Commands.User;
using Mediator.API.Model.Records.Requests;
using Mediator.API.Model.Records.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServices();

var app = builder.Build();

await app.SetupMiddlewares();
await app.SetupDatabaseAsync();

app.MapPost("user/login", async ([FromBody] UserLoginRequest loginRequest, [FromServices] IMediator mediator) =>
{
    try
    {
        var token = await mediator.Send(loginRequest);
        return Results.Ok(new { Token = token });
    }
    catch (Exception ex)
    {
        return Results.Unauthorized();
    }
});

app.MapPost("link/create", async ([FromBody] string link, IMediator mediator, HttpContext httpContext) =>
{
    var user = (User)httpContext.Items["User"]!;
    var newLink = await mediator.Send(new LinkCreateCommand(link, user.Id));

    return Results.Ok(newLink);
});

app.MapPost("user/registration", async ([FromBody]UserRegisterCommand command, [FromServices] IMediator mediator, HttpContext httpContext) =>
{
    try
    {
        var token = await mediator.Send(command);
        httpContext.Response.Cookies.Append("JWT-Token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddHours(1)
        });
        return Results.Ok(new { Token = token });
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapDelete("user/delete", async ([FromBody] UserDeleteCommand command, [FromServices] IMediator mediator) =>
{
    var result = await mediator.Send(command);
    return result == "Ok"? Results.Ok() : Results.BadRequest(result);
});

app.MapGet("/{link}", async (string link, LinkDbContext dbContext) =>
{
    var fullLink = await dbContext.Links.FirstOrDefaultAsync(l => l.ShorteredUrl == $"http://localhost:5196/{link}");
    return fullLink is null ? Results.BadRequest() : Results.Redirect(fullLink.OriginalUrl);
});

app.Run();
return;

