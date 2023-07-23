namespace CleanArchitecture.Application.Contracts.Common.Pagination;

public sealed record PagedResponse<T>
{
    public int TotalCount { get; }
    public int Page { get; }
    public int PageSize { get; }
    
    public int TotalPage => TotalCount / PageSize;
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;

    public IReadOnlyCollection<T> Items { get; }
    
    public static PagedResponse<T> Empty => new(Array.Empty<T>(), 0, 0, 0);

    public PagedResponse(IEnumerable<T> items, int totalCount, int page, int pageSize)
    {
        Items = items.ToList();
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }
}