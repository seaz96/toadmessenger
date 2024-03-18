using Application.Models;
using Application.Services.Interfaces;
using Core.HttpLogic.Services;
using Core.HttpLogic.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ChatsService(IChatsRepository chatsRepository, IHttpRequestService httpRequestService) : IChatsService
{
    private readonly IChatsRepository _chatsRepository = chatsRepository;
    private readonly IHttpRequestService _httpRequestService = httpRequestService;

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
    
    public async Task<ChatInfoModelWithUsersInfo?> GetChatInfoByIdWithUsersInfoAsync(Guid chatId)
    {
        var chatInfo = await _chatsRepository.GetChatByIdWithUsersAsync(chatId);

        var result = new ChatInfoModelWithUsersInfo{Id = chatInfo.Id, Messages = chatInfo.Messages, Users = new List<UserInfoModel>()};

        var data = new HttpRequestData
        {
            Method = HttpMethod.Post,
        };
        
        foreach (var user in chatInfo.Users)
        {
            data.Uri = new Uri("http://localhost:5102/api/identity-service/users/" + user.Id);
            
            var userInfoResponse = await _httpRequestService.SendRequestAsync<UserInfoModel>(data);
            
            if (userInfoResponse.Body is not null)
            {
                result.Users.Add(userInfoResponse.Body);
            }
        }
        
        return result;
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