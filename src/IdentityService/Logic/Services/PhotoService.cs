using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Logic.Helpers;
using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Logic.Services;

public class PhotoService(IPhotoRepository photoRepository, IUserRepository userRepository,
    IHttpContextAccessor httpContextAccessor) : IPhotoService
{
    private readonly IPhotoRepository _photoRepository = photoRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    
    public async Task<PhotoEntity?> CreatePhotoAsync(UpdateUserPhotoRequestModel updatePhotoModel)
    {
        var user = await UsersHelper.GetUserFromClaimsAsync(_httpContextAccessor, _userRepository);

        if (user is null)
        {
            return null;
        }
        
        var imageBytes = Convert.FromBase64String(updatePhotoModel.ImageBase64);
        var photo = new PhotoEntity
        {
            Bytes = imageBytes,
            FileExtension = updatePhotoModel.ImageBase64[0] == '/' ? "jpg" : "png",
            Size = imageBytes.Length,
            User = user,
            UserId = user.Id
        };
        
        await _photoRepository.AddPhotoAsync(photo);

        return photo;
    }

    public async Task<PhotoEntity?> GetPhotoByIdAsync(Guid photoId)
    {
        return await _photoRepository.GetPhotoByIdAsync(photoId);
    }
    
    public async Task<PhotoEntity?> GetPhotoByUserIdAsync(Guid userId)
    {
        return await _photoRepository.GetPhotoByUserIdAsync(userId);
    }

    public async Task UpdatePhotoAsync(UpdateUserPhotoRequestModel updatePhotoModel)
    {
        var user = await UsersHelper.GetUserFromClaimsAsync(_httpContextAccessor, _userRepository);
        var photo = await GetPhotoByUserIdAsync(user.Id);

        if (photo is null)
        {
            await CreatePhotoAsync(updatePhotoModel);
            return;
        }
        
        var imageBytes = Convert.FromBase64String(updatePhotoModel.ImageBase64);

        photo.Bytes = imageBytes;
        photo.Size = imageBytes.Length;
        
        await _photoRepository.UpdatePhotoAsync(photo);
    }

    public async Task DeletePhotoAsync(Guid id)
    {
        await _photoRepository.DeletePhotoAsync(id);
    }
}