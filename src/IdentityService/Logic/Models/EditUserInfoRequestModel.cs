using System.ComponentModel.DataAnnotations;

namespace Logic.Models;

public record EditUserInfoRequestModel
{
    [MaxLength(30)]
    public string? Name { get; set; }
    
    [MinLength(5)]
    [MaxLength(20)]
    public string? Username { get; set; }
    
    [StringLength(100)]
    public string? Description { get; set; }
    
}