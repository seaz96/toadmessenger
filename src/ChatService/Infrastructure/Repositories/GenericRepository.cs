using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class GenericRepository<TEntity>(DataContext context) : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DataContext _context = context;
    
    public virtual async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);

        await _context.SaveChangesAsync();
    }

    public virtual async Task<ICollection<TEntity>> GetAllAsync(int limit, int offset)
    {
        return await _context.Set<TEntity>().Skip(offset).Take(limit).ToListAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);

        await _context.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);

        await _context.SaveChangesAsync();
    }
}