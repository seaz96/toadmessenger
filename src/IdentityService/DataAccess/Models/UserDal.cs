using Core.Dal;
using Core.Dal.Base;

namespace DataAccess.Models;

public class UserDal : BaseEntity<Guid>
{
    public required string Name { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    public string Description { get; set; }
    
    public PhotoDal Photo { get; set; }
}