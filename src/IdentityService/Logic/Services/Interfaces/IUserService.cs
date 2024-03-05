using Core.Dal.Entities;
using Logic.Models;

namespace Logic.Services.Interfaces;

public interface IUserService
{
    public Task<UserIdWithTokenModel?> RegisterNewUserAsync(RegisterModel registerModel);

    public Task<UserIdWithTokenModel?> LoginUserAsync(LoginModel loginModel);
    
    public Task<Guid> CreateUserAsync(UserEntity user);

    public Task<UserInfoModel?> GetUserAsync(Guid id, bool withPhoto);

    public Task<UserInfoModel?> GetUserAsync(string phone, bool withPhoto);

    public Task UpdateUserInfoAsync(EditUserInfoRequestModel editModel);
    
    public Task<bool> UpdateUserEmailAsync(EditUserEmailRequestModel editModel);

    public Task<bool> ChangeUserPasswordAsync(ChangeUserPasswordRequestModel changePasswordModel);

    public Task<bool> DeleteUserAsync();
}