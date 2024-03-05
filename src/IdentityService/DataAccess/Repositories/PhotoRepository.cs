using Core.Dal.Entities;
using DataAccess.Persistence;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PhotoRepository(DataContext context) : IPhotoRepository
{
    private readonly DataContext _context = context;
    
    public async Task AddPhotoAsync(PhotoEntity photo)
    {
        await _context.Photos.AddAsync(photo);

        await _context.SaveChangesAsync();
    }
    
    public async Task<PhotoEntity?> GetPhotoByIdAsync(Guid id)
    {
        return await _context.Photos.FirstOrDefaultAsync(photo => photo.Id == id);
    }
    
    public async Task<PhotoEntity?> GetPhotoByUserIdAsync(Guid userId)
    {
        return await _context.Photos
            .Include(p => p.User)
            .FirstOrDefaultAsync(photo => photo.User.Id == userId);
    }

    public async Task DeletePhotoAsync(Guid id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(photo => photo.Id == id);

        if (photo is null)
        {
            return;
        }
        
        _context.Photos.Remove(photo);

        await _context.SaveChangesAsync();
    }
    
    public async Task UpdatePhotoAsync(PhotoEntity photo)
    {
        _context.Photos.Update(photo);

        await _context.SaveChangesAsync();
    }
}