using System.Security.Claims;
using System.Text.RegularExpressions;
using AutoMapper;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Logic.Helpers;
using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Logic.Services;

public class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher,
    IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor,
    IPhotoRepository photoRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPhotoRepository _photoRepository = photoRepository;
    private readonly IConfiguration _configuration = configuration;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<UserIdWithTokenModel?> RegisterNewUserAsync(RegisterModel registerModel)
    {
        if (await _userRepository.IsUserWithSuchPhoneExists(registerModel.Phone))
        {
            return null;
        }

        var user = new UserEntity
        {
            Phone = registerModel.Phone,
            Password = _passwordHasher.Hash(registerModel.Password),
            Name = registerModel.Name
        };

        await _userRepository.AddUserAsync(user);

        var token = SecurityHelper.GetAuthToken(user, _configuration);

        return new UserIdWithTokenModel
        {
            UserId = user.Id,
            Token = token
        };
    }
    
    public async Task<UserIdWithTokenModel?> LoginUserAsync(LoginModel loginModel)
    {
        const string phonePattern = """^[0-9]{3}[0-9]{3}[0-9]{4,6}$""";
        const string emailPattern = """^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$""";
        var user = new UserEntity{Name = "", Password = "", Phone = ""};

        if (Regex.IsMatch(loginModel.Login, phonePattern))
        {
            user = await _userRepository.GetUserByPhoneAsync(loginModel.Login, false);
        }
        else if (Regex.IsMatch(loginModel.Login, emailPattern))
        {
            user = await _userRepository.GetUserByEmailAsync(loginModel.Login, false);
        }
        else
        {
            return null;
        }
        
        if (user is null)
        {
            return null;
        }
        
        if (!_passwordHasher.Verify(loginModel.Password, user.Password))
        {
            return null;
        }
        
        var token = SecurityHelper.GetAuthToken(user, _configuration);

        return new UserIdWithTokenModel
        {
            UserId = user.Id,
            Token = token
        };
    }
    
    public async Task<Guid> CreateUserAsync(UserEntity user)
    {
        await _userRepository.AddUserAsync(user);

        return user.Id;
    }

    public async Task<UserInfoModel?> GetUserAsync(Guid id, bool withPhoto)
    {
        var user = await _userRepository.GetUserByIdAsync(id, withPhoto);
        var userInfo = _mapper.Map<UserInfoModel>(user);

        if (user is null)
        {
            return null;
        }

        if (user.Photo is not null)
        {
            userInfo.Photo = new PhotoDto
            {
                Base64 = Convert.ToBase64String(user.Photo.Bytes),
                Size = user.Photo.Size
            };
            
        }
        
        return userInfo;
    }
    
    public async Task<UserInfoModel?> GetUserAsync(string phone, bool withPhoto)
    {
        var user = await _userRepository.GetUserByPhoneAsync(phone, withPhoto);
        var userInfo = _mapper.Map<UserInfoModel>(user);
        
        if (user is null)
        {
            return null;
        }
        
        if (user.Photo is not null)
        {
            userInfo.Photo = new PhotoDto
            {
                Base64 = Convert.ToBase64String(user.Photo.Bytes),
                Size = user.Photo.Size
            };
            
        }
        
        return userInfo;
    }

    public async Task UpdateUserInfoAsync(EditUserInfoRequestModel editModel)
    {
        var user = await UsersHelper.GetUserFromClaimsAsync(_httpContextAccessor, _userRepository);

        if (user is null)
        {
            return;
        }
        
        user.Name = editModel.Name ?? user.Name;
        user.Description = editModel.Description ?? user.Description;
        user.Username = editModel.Username ?? user.Username;
        
        await _userRepository.UpdateUserAsync(user);
    }
    
    public async Task<bool> UpdateUserEmailAsync(EditUserEmailRequestModel editModel)
    {
        if (await _userRepository.IsUserWithSuchEmailExists(editModel.Email))
        {
            return false;
        }
        
        var user = await UsersHelper.GetUserFromClaimsAsync(_httpContextAccessor, _userRepository);
        
        if (user is null)
        {
            return false;
        }
        
        user.Email = editModel.Email;
        
        await _userRepository.UpdateUserAsync(user);

        return true;
    }
    
    public async Task<bool> ChangeUserPasswordAsync(ChangeUserPasswordRequestModel changePasswordModel)
    {
        var userIdFromClaim = _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
            ?.Value;

        if (userIdFromClaim is null)
        {
            return false;
        }

        var userId = new Guid(userIdFromClaim);
        var user = await _userRepository.GetUserByIdAsync(userId, false);

        if (user is null)
        {
            return false;
        }

        if (!_passwordHasher.Verify(changePasswordModel.OldPassword, user.Password))
        {
            return false;
        }

        user.Password = _passwordHasher.Hash(changePasswordModel.NewPassword);
        await _userRepository.UpdateUserAsync(user);

        return true;
    }
    
    public async Task<bool> DeleteUserAsync()
    {
        var user = await UsersHelper.GetUserFromClaimsAsync(_httpContextAccessor, _userRepository);

        if (user is null)
        {
            return false;
        }
        
        if (user.Photo is not null)
        {
            await _photoRepository.DeletePhotoAsync(user.Photo.Id);
        }
        
        await _userRepository.DeleteAsync(user.Id);

        return true;
    }
}