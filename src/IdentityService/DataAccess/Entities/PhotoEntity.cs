using Core.Dal.Base;

namespace DataAccess.Entities;

public class PhotoEntity : BaseEntity<Guid>
{
    public required string FileExtension { get; set; }

    public required double Size { get; set; }
    
    public required byte[] Bytes { get; set; }
    
    public required UserEntity User { get; set; }
    
    public required Guid UserId { get; set; }
}