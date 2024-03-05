using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DataAccess;

public static class DataAccessStartUp
{
    public static IServiceCollection TryAddDataAccessServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IUserRepository, UserRepository>();
        serviceCollection.TryAddScoped<IPhotoRepository, PhotoRepository>();
        
        return serviceCollection;
    }
}