using Domain.Entities;

namespace Domain.Interfaces;

public interface IChatsRepository : IGenericRepository<ChatEntity>
{
    public Task<ChatEntity?> GetChatByIdWithUsersAndMessagesAsync(Guid chatId);
    
    public Task<ChatEntity?> GetChatByIdWithUsersAsync(Guid chatId);
}