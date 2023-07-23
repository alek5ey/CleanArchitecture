namespace CleanArchitecture.FunctionalTests.Web.Helpers;

public class SerializablePagedResponse<T>
{
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    
    public int TotalPage { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }

    public List<T> Items { get; set; } = new();
}