using Application.Models;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/chat-service/chats")]
public class ChatsController(IChatsService chatsService) : ControllerBase
{
    private readonly IChatsService _chatsService = chatsService;
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateNewChatAsync([FromBody] CreateNewChatRequestModel requestModel)
    {
        var result = await _chatsService.CreateNewChatAsync(requestModel);

        return new OkObjectResult(result);
    }
    
    [HttpGet("{chatId}/info")]
    public async Task<IActionResult> GetChatInfoAsync([FromRoute] Guid chatId)
    {
        var result = await _chatsService.GetChatInfoByIdWithUsersInfoAsync(chatId);

        return new OkObjectResult(result);
    }
    
    [HttpGet("{chatId}/messages")]
    public async Task<IActionResult> GetChatMessagesAsync([FromRoute] Guid chatId)
    {
        var result = await _chatsService.GetChatMessagesByChatIdAsync(chatId);

        return new OkObjectResult(result);
    }
}