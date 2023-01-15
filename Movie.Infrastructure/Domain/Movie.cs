using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Infrastructure.Domain;

[Table(nameof(Movie))]
public sealed class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Rating { get; set; }
    public string? Synopsis { get; set; }
}