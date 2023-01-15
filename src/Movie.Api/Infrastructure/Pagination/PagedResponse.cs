namespace Movie.Api.Infrastructure.Pagination;

public class PagedResponse<TListItem>
{
    public int CurrentPage { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<TListItem> Data { get; set; }
}