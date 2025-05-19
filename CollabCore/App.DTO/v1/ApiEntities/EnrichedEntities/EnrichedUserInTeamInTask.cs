using Base.Contracts;

namespace App.DTO.v1.ApiEntities.EnrichedEntities;

public class EnrichedUserInTeamInTask : IDomainId
{
    public Guid Id { get; set; }
    
    public DateTime Since { get; set; }
    
    public DateTime? Until { get; set; }
    
    public string? Review { get; set; }
    
    public Guid TaskId { get; set; }
    
    public Guid UserInTeamId { get; set; }
    
    public Guid StatusId { get; set; }
    public string StatusName { get; set; } = default!;
    
    public List<string>? ParticipantNames { get; set; }
}