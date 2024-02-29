using AutoMapper;
using Logic.MappingProfiles;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace API.Helpers;

public static class MapperServiceStartUp
{
    public static void TryAddMapper(this IServiceCollection services)
    {
        var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new UserProfile());
            })
            .CreateMapper();

        services.TryAddSingleton(mapper);
    }
}