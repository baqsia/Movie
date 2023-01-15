using MediatR;
using Movie.Api.Dto;
using Movie.Api.Service;
using Movie.Infrastructure.Repository;

namespace Movie.Api.Query.MovieDetails;

public class GetMovieDetailsQueryHandler : IRequestHandler<GetMovieDetailsQuery, MovieDetailsModel?>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IGuidTransformService _guidTransformService;

    public GetMovieDetailsQueryHandler(IMovieRepository movieRepository, IGuidTransformService guidTransformService)
    {
        _movieRepository = movieRepository;
        _guidTransformService = guidTransformService;
    }

    public async Task<MovieDetailsModel?> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
    {
        var id = _guidTransformService.FromUriString(request.Id);
        var movie = await _movieRepository.GetById(id);
        return movie is not null
            ? new MovieDetailsModel(request.Id, movie.Title, movie.Rating, movie.ReleaseDate, movie.Synopsis)
            : null;
    }
}