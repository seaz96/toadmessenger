using Core.Dal.Base;

namespace Domain;

public interface IGenericRepository<TEntity> where TEntity : class
{
    public Task AddAsync(TEntity entity);

    public Task<ICollection<TEntity>> GetAllAsync(int limit, int offset);

    public Task DeleteAsync(TEntity entity);

    public Task Update(TEntity entity);
}