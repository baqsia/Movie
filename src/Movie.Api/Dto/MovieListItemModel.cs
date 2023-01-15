namespace Movie.Api.Dto;

public record MovieListItemModel(string Id, string Title, string? Rating, DateTime ReleaseDate, string? Synopsis);