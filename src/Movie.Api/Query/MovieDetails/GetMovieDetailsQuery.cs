using MediatR;
using Movie.Api.Dto;

namespace Movie.Api.Query.MovieDetails;

public class GetMovieDetailsQuery : IRequest<MovieDetailsModel?>
{
    public string Id { get; set; }
}