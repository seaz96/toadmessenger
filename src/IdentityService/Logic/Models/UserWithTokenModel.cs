using Core.Dal.Entities;

namespace Logic.Models;

public record UserIdWithTokenModel
{
    public Guid UserId { get; set; }

    public string Token { get; set; } = null!;
}