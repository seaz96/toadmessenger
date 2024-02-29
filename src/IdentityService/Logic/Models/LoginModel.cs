using System.ComponentModel.DataAnnotations;

namespace Logic.Models;

public record LoginModel
{
    public required string Login { get; set; }
    
    [MinLength(5)]
    [MaxLength(20)]
    public required string Password { get; set; }
}