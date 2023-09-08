using CleanArchitecture.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories;

public abstract class BaseRepository<TEntity, TEntityId>
    where TEntity : DomainEntity<TEntityId>
    where TEntityId : class
{
    protected readonly ApplicationDbContext Context;
    public IQueryable<TEntity> Query => Context.Set<TEntity>().AsQueryable().AsNoTracking();

    protected BaseRepository(ApplicationDbContext context)
    {
        Context = context;
    }

    public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default) =>
        await Context.Set<TEntity>().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

    public async Task AddAsync(TEntity customer, CancellationToken cancellationToken = default) =>
        await Context.Set<TEntity>().AddAsync(customer, cancellationToken);

    public void Update(TEntity customer) =>
        Context.Set<TEntity>().Update(customer);

    public void Remove(TEntity customer) =>
        Context.Set<TEntity>().Remove(customer);
}