namespace CleanArchitecture.Application.Contracts.Common.Pagination;

public abstract record PaginatableRequest
{
    public int Page { get; private init; }
    public int PageSize { get; private init; }
    public string OrderBy { get; private init; }

    public const int DefaultPage = 1;
    public const int DefaultPageSize = 20;
    public const int MaxPageSize = 100;

    protected PaginatableRequest(
        int page,
        int pageSize,
        string? orderBy,
        string defaultOrderBy,
        Dictionary<string, string> mappedOrderByColumns)
    {
        Page = page <= 0 ? DefaultPage : page;
        PageSize = pageSize <= 0 ? DefaultPageSize : (pageSize > MaxPageSize ? MaxPageSize : pageSize);
        OrderBy = FormatOrderBy(orderBy, defaultOrderBy, mappedOrderByColumns);
    }
    
    private static string FormatOrderBy(
        string? orderByProperties,
        string defaultOrderBy,
        Dictionary<string, string> mappedOrderByColumns)
    {
        if (string.IsNullOrWhiteSpace(orderByProperties))
            return defaultOrderBy;
        
        var result = new List<string>(mappedOrderByColumns.Count);

        foreach (var (property, isAscSort) in orderByProperties
                     .Split(',', StringSplitOptions.RemoveEmptyEntries)
                     .Select(p => p.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                     .Where(p => p.Length is > 0 and <= 2)
                     .Select(p => (p[0], p.Length == 1 || p[1] == "asc")))
        {
            if (mappedOrderByColumns.TryGetValue(property.ToLower(), out var orderByProperty))
                result.Add($"{orderByProperty} {(isAscSort ? "asc" : "desc")}");
        }
        
        if (!result.Any() && !string.IsNullOrWhiteSpace(defaultOrderBy))
            result.Add(defaultOrderBy);

        return string.Join(',', result);
    }
}