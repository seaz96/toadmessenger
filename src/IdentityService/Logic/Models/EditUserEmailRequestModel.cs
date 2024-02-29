using System.ComponentModel.DataAnnotations;

namespace Logic.Models;

public record EditUserEmailRequestModel
{
    [EmailAddress]
    public string Email { get; set; } = null!;
}