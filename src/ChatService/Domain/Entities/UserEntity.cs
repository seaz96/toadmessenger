using Core.Dal.Base;

namespace Domain.Entities;

public class UserEntity : BaseEntity<Guid>
{
    public required string Name { get; set; }
}