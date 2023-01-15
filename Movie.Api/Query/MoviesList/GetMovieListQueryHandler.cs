using MediatR;
using Movie.Api.Dto;
using Movie.Api.Infrastructure.Pagination;
using Movie.Api.Service;
using Movie.Infrastructure.Repository;

namespace Movie.Api.Query;

public class GetMovieListQueryHandler : IRequestHandler<GetMovieListQuery, PagedResponse<MovieListItemModel>>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IGuidTransformService _guidTransformService;

    public GetMovieListQueryHandler(IMovieRepository movieRepository, IGuidTransformService guidTransformService)
    {
        _movieRepository = movieRepository;
        _guidTransformService = guidTransformService;
    }

    public async Task<PagedResponse<MovieListItemModel>> Handle(GetMovieListQuery request,
        CancellationToken cancellationToken)
    {
        var (totalCount, movies) = await _movieRepository
            .GetPaged(cancellationToken, request.Page, request.ItemCount, request.Query);

        var result = movies.Select(movie => new MovieListItemModel(
            _guidTransformService.ToUriString(movie.Id),
            movie.Title,
            movie.Rating,
            movie.ReleaseDate,
            movie.Synopsis
        ));
        return new PagedResponse<MovieListItemModel>
        {
            TotalCount = totalCount,
            Data = result,
            CurrentPage = request.Page
        };
    }
}