using AutoMapper;
using DataAccess.Entities;
using Logic.Helpers;
using Logic.Models;

namespace Logic.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserInfoModel>()
            .Ignore(entity => entity.Photo);
    }
}