using Microsoft.EntityFrameworkCore;
using Movie.Infrastructure.Context.Configuration;

namespace Movie.Infrastructure.Context;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieEntityConfiguration).Assembly);
    }
}
