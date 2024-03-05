using System.ComponentModel.DataAnnotations;
using Core.Dal.Base;

namespace Core.Dal.Entities;

public class UserEntity : BaseEntity<Guid>
{
    [MaxLength(30)]
    public required string Name { get; set; }
    
    [MinLength(5)]
    [MaxLength(20)]
    public string? Username { get; set; }
    
    public required string Password { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    public required string Phone { get; set; }
    
    [StringLength(100)]
    public string? Description { get; set; }
    
    public PhotoEntity? Photo { get; set; }
}