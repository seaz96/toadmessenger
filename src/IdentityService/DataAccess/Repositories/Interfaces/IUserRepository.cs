using Core.Dal.Entities;

namespace DataAccess.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<List<UserEntity>> GetAllAsync(int limit, int offset, bool withPhoto);
    
    public Task<UserEntity?> GetUserByPhoneAsync(string phone, bool withPhoto);

    public Task<UserEntity?> GetUserByEmailAsync(string email, bool withPhoto);
    
    public Task<UserEntity?> GetUserByIdAsync(Guid id, bool withPhoto);

    public Task AddUserAsync(UserEntity user);

    public Task DeleteAsync(Guid id);

    public Task UpdateNameAsync(Guid id, string name);

    public Task UpdatePasswordAsync(Guid id, string password);

    public Task UpdateUserAsync(UserEntity user);

    public Task<bool> IsUserWithSuchPhoneExists(string phone);
    
    public Task<bool> IsUserWithSuchEmailExists(string email);
}