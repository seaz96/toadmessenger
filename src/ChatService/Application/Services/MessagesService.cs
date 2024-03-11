using Application.Models;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class MessagesService(IChatsRepository chatsRepository, IMessagesRepository messagesRepository) : IMessagesService
{
    private readonly IChatsRepository _chatsRepository = chatsRepository;
    private readonly IMessagesRepository _messagesRepository = messagesRepository;

    public async Task<int?> AddMessageToChatAsync(AddMessageToChatRequestModel requestModel)
    {
        var chat = await _chatsRepository.GetChatByIdWithUsersAndMessagesAsync(requestModel.ChatId);

        if (chat is null)
        {
            return null;
        }
        
        var message = new MessageEntity
        {
            Text = requestModel.Text,
            UserId = requestModel.UserId,
            Date = DateTime.UtcNow,
            ChatId = requestModel.ChatId,
            Chat = chat
        };

        await _messagesRepository.AddAsync(message);

        return message.Id;
    }
}