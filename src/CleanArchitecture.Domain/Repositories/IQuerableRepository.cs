namespace CleanArchitecture.Domain.Repositories;

public interface IQuerableRepository<out T>
{
    IQueryable<T> Query { get; }
}