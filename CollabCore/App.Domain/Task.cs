using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Task : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public string? Description { get; set; }
    
    public DateTime AssignedAt { get; set; }
    public DateTime Deadline { get; set; }
    
    public Guid StatusId { get; set; }
    public Status? Status { get; set; }
    
    public Guid UserInTeamId { get; set; }
    public UserInTeam? UserInTeam { get; set; }
    
    public ICollection<UserInTeamInTask>? UserInTasks { get; set; }
}