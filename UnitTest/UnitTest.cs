using Mediator.API.Handlers.Command;
using Mediator.API.Model;
using Mediator.API.Model.Records.Commands.User;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace UnitTest;

public class UnitTest
{
    private readonly LinkDbContext _dbContext;
    private readonly UserDeleteCommandHandler _handler;

    public UnitTest()
    {
        var options = new DbContextOptionsBuilder<LinkDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _dbContext = new LinkDbContext(options);
        _handler = new UserDeleteCommandHandler(_dbContext);
    }

    [Fact]
    public async Task Handle_ReturnsOk_WhenUserExists()
    {
        var user = new User { Email = "test@example.com", Password = "testPassword"};
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        var command = new UserDeleteCommand( "test@example.com" );
        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Ok", result);
        Assert.Empty(_dbContext.Users.Where(u => u.Email == "test@example.com"));
    }

    [Fact]
    public async Task Handle_ReturnsNotFound_WhenUserDoesNotExist()
    {
        var command = new UserDeleteCommand("nonexistent@example.com");
        var result = await _handler.Handle(command, CancellationToken.None);
        Assert.Equal("NotFound", result);
    }
    
}