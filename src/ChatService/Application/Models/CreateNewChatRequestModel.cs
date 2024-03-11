using Domain.Entities;

namespace Application.Models;

public class CreateNewChatRequestModel
{
    public required ICollection<Guid> UserIds { get; set; }
}