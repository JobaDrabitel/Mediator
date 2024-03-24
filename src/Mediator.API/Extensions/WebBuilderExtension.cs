using Mediator.API.Middlewares;
using Mediator.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Mediator.API.Extensions;

public static class WebBuilderExtension
{
    public static async Task<IApplicationBuilder> SetupDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var conn = scope.ServiceProvider.GetRequiredService<LinkDbContext>();

        await conn.Database.MigrateAsync();

        return app;
    }

    public static Task<IApplicationBuilder> SetupMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<JwtMiddleware>();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.MapControllers();

        return Task.FromResult<IApplicationBuilder>(app);
    }
}
