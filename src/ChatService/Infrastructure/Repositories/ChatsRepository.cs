using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChatsRepository(DataContext context) : GenericRepository<ChatEntity>(context), IChatsRepository
{
    public async Task<ChatEntity?> GetChatByIdWithUsersAndMessagesAsync(Guid chatId)
    {
        return await Context.Chats
            .Include(c => c.Users)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == chatId);
    }
    
    public async Task<ChatEntity?> GetChatByIdWithUsersAsync(Guid chatId)
    {
        return await Context.Chats
            .Include(c => c.Users)
            .FirstOrDefaultAsync(c => c.Id == chatId);
    }
}