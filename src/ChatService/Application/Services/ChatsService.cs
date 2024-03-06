using Application.Models;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ChatsService(IChatsRepository chatsRepository) : IChatsService
{
    private readonly IChatsRepository _chatsRepository = chatsRepository;

    public async Task<Guid> CreateNewChatAsync(CreateNewChatRequestModel requestModel)
    {
        var users = requestModel.UserIds.Select(x => new UserEntity { Id = x }).ToList();
        var newChat = new ChatEntity
        {
            Messages = new List<MessageEntity>(),
            Users = users
        };
        
        await _chatsRepository.AddAsync(newChat);

        return newChat.Id;
    }

    public async Task<ChatEntity?> GetChatInfoByIdAsync(Guid chatId)
    {
        return await _chatsRepository.GetChatByIdWithUsersAsync(chatId);
    }
    
    public async Task<ICollection<MessageDtoModel>?> GetChatMessagesByChatIdAsync(Guid chatId)
    {
        var chat = await _chatsRepository.GetChatByIdWithUsersAndMessagesAsync(chatId);

        return chat?.Messages.Select(m => new MessageDtoModel
        {
            Date = m.Date,
             Text = m.Text,
             UserId = m.UserId
        }).ToList();
    }
}