using Application.Services;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application;

public static class ApplicationStartUp
{
    public static IServiceCollection TryAddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IChatsService, ChatsService>();
        serviceCollection.TryAddScoped<IMessagesService, MessagesService>();
        
        return serviceCollection;
    }
}