using System.ComponentModel.DataAnnotations;
using Core.Dal.Base;

namespace Domain.Entities;

public class MessageEntity : BaseEntity<int>
{
    [MaxLength(255)]
    public required string Text { get; set; }
    
    public required Guid UserId { get; set; }
    
    public required DateTime Date { get; set; }
    
    public required ChatEntity Chat { get; set; }
    
    public required Guid ChatId { get; set; }
}