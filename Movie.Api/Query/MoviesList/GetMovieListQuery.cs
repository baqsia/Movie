using MediatR;
using Movie.Api.Dto;
using Movie.Api.Infrastructure.Pagination;

namespace Movie.Api.Query;

public class GetMovieListQuery : PagedRequest,
    IRequest<PagedResponse<MovieListItemModel>>
{
    public GetMovieListQuery(string? query)
    {
        Query = query;
    }

    public string? Query { get; }
}