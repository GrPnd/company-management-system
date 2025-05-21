using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class UserInTeam : IDomainId
{
    public Guid Id { get; set; }
    
    public DateTime Since { get; set; }
    
    public DateTime? Until { get; set; }
    
    public Guid TeamRoleId { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid TeamId { get; set; }
}