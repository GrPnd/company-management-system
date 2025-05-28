using Base.Contracts;

namespace App.DAL.DTO;

public class UserInTeam : IDomainId
{
    public Guid Id { get; set; }
    
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
    
    public Guid UserId { get; set; }
}
