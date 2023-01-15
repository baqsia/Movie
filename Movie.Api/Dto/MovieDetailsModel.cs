namespace Movie.Api.Dto;
 
public record MovieDetailsModel(string Id, string Title, string? Rating, DateTime ReleaseDate, string? Synopsis);