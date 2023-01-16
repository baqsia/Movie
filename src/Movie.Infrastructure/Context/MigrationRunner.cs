using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Movie.Infrastructure.Context;

public class MigrationRunner
{
    public static async Task Run(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
    }
}