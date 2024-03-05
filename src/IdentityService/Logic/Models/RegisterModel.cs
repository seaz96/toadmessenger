using System.ComponentModel.DataAnnotations;

namespace Logic.Models;

public record RegisterModel
{
    [MaxLength(30)]
    public required string Name { get; set; }
    
    [RegularExpression("""^[0-9]{3}[0-9]{3}[0-9]{4,6}$""", ErrorMessage = "Invalid Phone Number.")]
    public required string Phone { get; set; }
    
    [MinLength(5)]
    [MaxLength(20)]
    public required string Password { get; set; }
}