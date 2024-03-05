using Logic.Services;
using Logic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Logic;

public static class LogicStartUp
{
    public static IServiceCollection TryAddLogicServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IPasswordHasher, Sha256PasswordHasher>();
        serviceCollection.TryAddScoped<IUserService, UserService>();
        serviceCollection.TryAddScoped<IPhotoService, PhotoService>();
        
        return serviceCollection;
    }
}