using Core.Dal.Entities;
using Logic.Models;

namespace Logic.Services.Interfaces;

public interface IPhotoService
{
    public Task<PhotoEntity?> CreatePhotoAsync(UpdateUserPhotoRequestModel updatePhotoModel);

    public Task<PhotoEntity?> GetPhotoByIdAsync(Guid photoId);

    public Task<PhotoEntity?> GetPhotoByUserIdAsync(Guid userId);

    public Task UpdatePhotoAsync(UpdateUserPhotoRequestModel updatePhotoModel);

    public Task DeletePhotoAsync(Guid id);
}