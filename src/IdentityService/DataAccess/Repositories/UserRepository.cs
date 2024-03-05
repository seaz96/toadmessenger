using DataAccess.Entities;
using DataAccess.Persistence;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
    private readonly DataContext _context = context;
    
    public async Task<List<UserEntity>> GetAllAsync(int limit, int offset, bool withPhoto)
    {
        IQueryable<UserEntity> users = _context.Users;
        
        if (withPhoto)
        {
            users = users.Include(u => u.Photo);
        }
        
        return await users
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }
    
    public async Task<UserEntity?> GetUserByIdAsync(Guid id, bool withPhoto)
    {
        IQueryable<UserEntity> users = _context.Users;

        if (withPhoto)
        {
            users = users.Include(u => u.Photo);
        }
        
        return await users.FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<UserEntity?> GetUserByPhoneAsync(string phone, bool withPhoto)
    {
        IQueryable<UserEntity> users = _context.Users;

        if (withPhoto)
        {
            users = users.Include(u => u.Photo);
        }
        
        return await users.FirstOrDefaultAsync(u => u.Phone == phone);
    }
    
    public async Task<UserEntity?> GetUserByEmailAsync(string email, bool withPhoto)
    {
        IQueryable<UserEntity> users = _context.Users;

        if (withPhoto)
        {
            users = users.Include(u => u.Photo);
        }
        
        return await users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddUserAsync(UserEntity user)
    {
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

        if (user is not null)
        {
            _context.Users.Remove(user);
        }

        await _context.SaveChangesAsync();
    }

    public async Task UpdateNameAsync(Guid id, string name)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

        if (user is null)
        {
            return;
        }
        
        user.Name = name;

        await _context.SaveChangesAsync();
    }

    public async Task UpdatePasswordAsync(Guid id, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user is not null)
        {
            user.Password = password;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateUserAsync(UserEntity user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsUserWithSuchPhoneExists(string phone)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Phone == phone) != null;
    }
    
    public async Task<bool> IsUserWithSuchEmailExists(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email) != null;
    }
}