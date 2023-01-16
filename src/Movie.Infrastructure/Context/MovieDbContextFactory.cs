using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Movie.Infrastructure.Context;


public class MovieDbContextFactory : IDesignTimeDbContextFactory<MovieDbContext>
{
    public MovieDbContext CreateDbContext(string[] args)
    { 
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile($"appsettings.Localhost.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MovieDbContext>();
 
        optionsBuilder.UseSqlServer(config.GetConnectionString("MovieDatabase")!, opt =>
        {
            opt.EnableRetryOnFailure(3);
            opt.MigrationsAssembly(typeof(MovieDbContext).Assembly.FullName);
        });
 
        return new MovieDbContext(optionsBuilder.Options);
    }
}