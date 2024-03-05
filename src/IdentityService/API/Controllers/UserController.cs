using System.Security.Claims;
using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/identity-service/users")]
public class UserController(IUserService userService, IPhotoService photoService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IPhotoService _photoService = photoService;

    [HttpPost("{id}")]
    public async Task<IActionResult> GetUserInfoAsync([FromRoute] Guid id, [FromQuery] bool? withPhoto)
    {
        var result = await _userService.GetUserAsync(id, withPhoto ?? false);

        if (result is null)
        {
            return BadRequest("User with such id not found.");
        }

        return new OkObjectResult(result);
    }
    
    [Authorize]
    [HttpPatch("edit-info")]
    public async Task<IActionResult> UpdateUserInfoAsync([FromBody] EditUserInfoRequestModel editModel)
    {
        await _userService.UpdateUserInfoAsync(editModel);

        return new OkObjectResult("Info updated.");
    }
    
    [Authorize]
    [HttpPatch("edit-email")]
    public async Task<IActionResult> UpdateUserEmailAsync([FromBody] EditUserEmailRequestModel editModel)
    {
        var success = await _userService.UpdateUserEmailAsync(editModel);

        if (!success)
        {
            return BadRequest("Email is already claimed.");
        }
        
        return new OkObjectResult("Email updated.");
    }
    
    [Authorize]
    [HttpPatch("update-photo")]
    public async Task<IActionResult> UpdateUserPhotoAsync([FromBody] UpdateUserPhotoRequestModel updatePhotoModel)
    {
        await _photoService.UpdatePhotoAsync(updatePhotoModel);
        
        return new OkObjectResult("Photo updated.");
    }
    
    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUserAsync()
    {
        var success = await _userService.DeleteUserAsync();

        if (!success)
        {
            return BadRequest();
        }
        
        return new OkObjectResult("User deleted.");
    }
}