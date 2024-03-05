using Core.Dal.Base;

namespace Domain.Entities;

public class ChatEntity : BaseEntity<Guid>
{
    public required ICollection<UserEntity> Users { get; set; }
    
    public required ICollection<MessageEntity> Messages { get; set; }
}