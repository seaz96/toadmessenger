using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class MessagesRepository(DataContext context) : GenericRepository<MessageEntity>(context), IMessagesRepository
{
    
}