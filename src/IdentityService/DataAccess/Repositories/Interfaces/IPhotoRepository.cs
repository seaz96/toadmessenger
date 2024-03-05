using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces;

public interface IPhotoRepository
{
    public Task AddPhotoAsync(PhotoEntity photo);

    public Task<PhotoEntity?> GetPhotoByIdAsync(Guid id);
    
    public Task<PhotoEntity?> GetPhotoByUserIdAsync(Guid userId);

    public Task DeletePhotoAsync(Guid id);

    public Task UpdatePhotoAsync(PhotoEntity photo);
}