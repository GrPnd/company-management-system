using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class UserInTeam : BaseEntity
{
    [MaxLength(128)]
    public string Role { get; set; } = default!;  // TODO: TULEB FK??
    
    public DateTime Since { get; set; }
    public DateTime? To { get; set; }
    
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
    
    public ICollection<Task>? Tasks { get; set; }
    public ICollection<UserInTeamInTask>? UserInTeamInTasks { get; set; }
}