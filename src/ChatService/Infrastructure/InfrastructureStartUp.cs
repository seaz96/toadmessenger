using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure;

public static class InfrastructureStartUp
{
    public static IServiceCollection TryAddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IChatsRepository, ChatsRepository>();
        serviceCollection.TryAddScoped<IMessagesRepository, MessagesRepository>();
        
        return serviceCollection;
    }
}