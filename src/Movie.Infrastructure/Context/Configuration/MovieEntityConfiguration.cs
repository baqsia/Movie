using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movie.Infrastructure.Context.Configuration;

public class MovieEntityConfiguration : IEntityTypeConfiguration<Domain.Movie>
{
    public void Configure(EntityTypeBuilder<Domain.Movie> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();
        builder.HasIndex(a => a.Id)
            .IsUnique();

        builder.Property(a => a.Title)
            .IsRequired();

        builder.Property(a => a.ReleaseDate)
            .IsRequired();

        var date = DateTime.Now;
        var random = Random.Shared;
        var movies = SeedMovieData.Seed("movies.json")
            .Select(movie =>
            {
                int.TryParse(movie.year, out var year);
                var exactYear = new DateTime(year, date.Month, date.Day);
                
                var movieEntity = new Domain.Movie
                {
                    Id = Guid.NewGuid(),
                    Title = movie.title,
                    Synopsis = movie.plot,
                    ReleaseDate = exactYear,
                    Rating = random.Next(0, 10).ToString()
                };
                return movieEntity;
            }).ToArray();
        builder.HasData(movies);
    }
}