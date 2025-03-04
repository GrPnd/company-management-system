using Base.Domain;

namespace App.Domain;

public class UserInRole : BaseEntity
{
    public DateTime Since { get; set; }
    public DateTime Until { get; set; }
    
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
}