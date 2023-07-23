using CleanArchitecture.Application.Contracts.Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Extensions;

internal static class LinqExtensions
{
    public static async Task<PagedResponse<T>> ToPage<T>(
        this IQueryable<T> query, PaginatableRequest request, CancellationToken cancellationToken = default)
    {
        var listItems = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        int totalCount;
        if (listItems.Count < request.PageSize)
            totalCount = listItems.Count;
        else
            totalCount = await query.CountAsync(cancellationToken);

        return new PagedResponse<T>(
            listItems,
            totalCount,
            request.Page,
            request.PageSize);
    }
}