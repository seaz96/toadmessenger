using Domain.Entities;

namespace Application.Models;

public record ChatInfoModelWithUsersInfo
{
    public required Guid Id { get; set; }
    
    public required ICollection<UserInfoModel> Users { get; set; }
    
    public required ICollection<MessageEntity> Messages { get; set; }
}