using System.Security.Claims;
using Core.Dal.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Logic.Helpers;

public static class UsersHelper
{
    public static async Task<UserEntity?> GetUserFromClaimsAsync(IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository)
    {
        var userIdFromClaim = httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
            ?.Value;

        if (userIdFromClaim is null)
        {
            return null;
        }

        var userId = new Guid(userIdFromClaim);
        return await userRepository.GetUserByIdAsync(userId, false);
    }
}