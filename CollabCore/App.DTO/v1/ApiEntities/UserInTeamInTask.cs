using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class UserInTeamInTask : IDomainId
{
    public Guid Id { get; set; }
    
    public DateTime Since { get; set; }
    
    public DateTime? Until { get; set; }
    
    public Guid TaskId { get; set; }
    
    public Guid UserInTeamId { get; set; }
}