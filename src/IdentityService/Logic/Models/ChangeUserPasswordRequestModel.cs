using System.ComponentModel.DataAnnotations;

namespace Logic.Models;

public record ChangeUserPasswordRequestModel
{
    [MinLength(5)]
    [MaxLength(20)] 
    public string OldPassword { get; set; } = null!;


    [MinLength(5)]
    [MaxLength(20)]
    public string NewPassword { get; set; } = null!;
}