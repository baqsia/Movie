using System.Reflection;
using System.Text.Json;

namespace Movie.Infrastructure;

public class SeedMovieData
{
    public static IEnumerable<(string title, string year, string plot)> Seed(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var directory = Path.GetDirectoryName(assembly.Location) ?? string.Empty;
        var filePath = Path.Combine(directory, fileName);
        var file = File.ReadAllText(filePath);

        var list = JsonSerializer.Deserialize<MovieSeedData>(file, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return list!.Movies.Select(movie => (movie.Title, movie.Year, movie.Plot));
    }

    internal class MovieSeedData
    {
        public IEnumerable<MovieSeedItem> Movies { get; set; }

        internal class MovieSeedItem
        {
            public string Title { get; set; }
            public string Year { get; set; }
            public string Plot { get; set; }
        }
    }
}