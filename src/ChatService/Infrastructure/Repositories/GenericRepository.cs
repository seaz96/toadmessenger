using Domain;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class GenericRepository<TEntity>(DataContext context) : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly DataContext Context = context;
    
    public virtual async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);

        await Context.SaveChangesAsync();
    }

    public virtual async Task<ICollection<TEntity>> GetAllAsync(int limit, int offset)
    {
        return await Context.Set<TEntity>().Skip(offset).Take(limit).ToListAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);

        await Context.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);

        await Context.SaveChangesAsync();
    }
}