using Base.Contracts;

namespace App.DTO.v1.ApiEntities.EnrichedEntities;

public class EnrichedUserInTeam : IDomainId
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public string PersonName { get; set; } = default!;
    
    public Guid TeamId { get; set; }
    public string TeamName { get; set; } = default!;
    
    public Guid TeamRoleId { get; set; }
    public string RoleName { get; set; } = default!;
    
    public DateTime Since { get; set; }
    
    public DateTime? Until { get; set; }
}