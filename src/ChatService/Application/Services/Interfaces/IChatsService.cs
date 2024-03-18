using Application.Models;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IChatsService
{
    public Task<Guid> CreateNewChatAsync(CreateNewChatRequestModel requestModel);

    public Task<ChatEntity?> GetChatInfoByIdAsync(Guid chatId);

    public Task<ChatInfoModelWithUsersInfo?> GetChatInfoByIdWithUsersInfoAsync(Guid chatId);

    public Task<ICollection<MessageDtoModel>?> GetChatMessagesByChatIdAsync(
        Guid chatId);
}