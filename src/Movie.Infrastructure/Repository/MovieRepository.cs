using Microsoft.EntityFrameworkCore;
using Movie.Infrastructure.Context;

namespace Movie.Infrastructure.Repository;

public interface IMovieRepository
{
    /// <summary>
    /// get or query movies list
    /// </summary>
    /// <returns>Task of List<list type="Domain.Movie"></list></returns>
    Task<(int totalCount, IEnumerable<Domain.Movie> movies)> GetPaged(CancellationToken ctx, int page, int itemCount,
        string? query);

    Task<Domain.Movie?> GetById(Guid movieId);
}

public class MovieRepository : IMovieRepository
{
    private readonly MovieDbContext _movieDbContext;

    public MovieRepository(MovieDbContext movieDbContext)
    {
        _movieDbContext = movieDbContext;
    }

    /// <inheritdoc/>
    public async Task<(int totalCount, IEnumerable<Domain.Movie> movies)> GetPaged(CancellationToken ctx, int page,
        int itemCount, string? query)
    {
        var baseQuery = _movieDbContext.Set<Domain.Movie>()
            .AsNoTracking();

        if (!string.IsNullOrEmpty(query))
        {
            baseQuery = baseQuery.Where(a =>
                a.Title.Contains(query)
                || a.ReleaseDate.Year.ToString() == query);
        }

        var totalCount = await baseQuery.CountAsync(ctx);

        if (totalCount == 0)
        {
            return (totalCount, Enumerable.Empty<Domain.Movie>());
        }
        
        return (totalCount, await baseQuery
            .OrderBy(a => a.Title)
            .ThenByDescending(a => a.ReleaseDate)
            .Skip(itemCount * (page - 1))
            .Take(itemCount)
            .ToListAsync(ctx));
    }

    public Task<Domain.Movie?> GetById(Guid movieId)
    {
        return _movieDbContext.Set<Domain.Movie>()
            .FirstOrDefaultAsync(a => a.Id == movieId);
    }
}