using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class MessageDtoModel
{
    [MaxLength(255)]
    public required string Text { get; set; }
    
    public required Guid UserId { get; set; }
    
    public required DateTime Date { get; set; }
}