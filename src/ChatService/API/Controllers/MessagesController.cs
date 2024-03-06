using Application.Models;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/chat-service/messages")]
public class MessagesController(IMessagesService messagesService) : ControllerBase
{
    private readonly IMessagesService _messagesService = messagesService;
    
    [HttpPost("add")]
    public async Task<IActionResult> CreateNewChatAsync([FromBody] AddMessageToChatRequestModel requestModel)
    {
        var result = await _messagesService.AddMessageToChatAsync(requestModel);

        return new OkObjectResult(result);
    }
}