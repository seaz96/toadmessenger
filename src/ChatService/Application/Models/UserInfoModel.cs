namespace Application.Models;

public record UserInfoModel
{
    public required string Name { get; set; }
    
    public string? Username { get; set; }
    
    public string? Email { get; set; }
    
    public required string Phone { get; set; }
    
    public string? Description { get; set; }
    
    public PhotoDto? Photo { get; set; }
}