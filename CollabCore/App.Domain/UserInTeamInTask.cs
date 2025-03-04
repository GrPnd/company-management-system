using Base.Domain;

namespace App.Domain;

public class UserInTeamInTask : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public string? Review { get; set; } = default!;
    
    public Guid TaskId { get; set; }
    public Task? Task { get; set; }
    
    public Guid UserInTeamId { get; set; }
    public UserInTeam? UserInTeam { get; set; }
}