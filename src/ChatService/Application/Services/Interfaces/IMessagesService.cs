using Application.Models;

namespace Application.Services.Interfaces;

public interface IMessagesService
{
    public Task<int?> AddMessageToChatAsync(AddMessageToChatRequestModel requestModel);
}