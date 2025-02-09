using Application.Abstractions.Authentication;

using Infrastructure.Database;
using System;

namespace Web.Api.Middleware;

internal static class MiddlewareConfig
{
    public static async Task<IApplicationBuilder> UseAppMiddlewareAndSeedDatabase(this WebApplication app)
    {
        // only seed the database in development
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            await SeedDatabase(app);
        }

        return app;
    }

    internal static async Task SeedDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var passwordHasher = services.GetRequiredService<IPasswordHasher>();
            await context.Database.EnsureCreatedAsync();
            await SeedData.InitializeAsync(context, passwordHasher).ConfigureAwait(true);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred seeding the DB. {ExceptionMessage}", ex.Message);
        }
    }
}
