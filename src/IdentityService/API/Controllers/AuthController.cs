using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/identity-service/auth")]
public class AuthController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUserWithPhoneAsync([FromBody] LoginModel loginModel)
    {
        var result = await _userService.LoginUserAsync(loginModel);

        if (result is null)
        {
            return BadRequest("Login or password is incorrect.");
        }

        return new OkObjectResult(result);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterModel registerModel)
    {
        var result = await _userService.RegisterNewUserAsync(registerModel);

        if (result is null)
        {
            return BadRequest("User already exists.");
        }

        return new OkObjectResult(result);
    }
    
    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangeUserPasswordAsync([FromBody] ChangeUserPasswordRequestModel changePasswordModel)
    {
        var result = await _userService.ChangeUserPasswordAsync(changePasswordModel);

        if (result is false)
        {
            return BadRequest("Old password is incorrect.");
        }

        return new OkObjectResult("Password changed.");
    }
}