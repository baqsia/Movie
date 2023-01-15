using Microsoft.EntityFrameworkCore;
using Movie.Infrastructure.Context;

namespace Movie.Api.IntegrationTests;

public static class ContextFactory
{
    public static MovieDbContext CreateInMemory()
    {
        var options = new DbContextOptionsBuilder<MovieDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;


        return new MovieDbContext(options);
    }
}