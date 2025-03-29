using System.ComponentModel.DataAnnotations;
using Base.Domain;
using DateTime = System.DateTime;

namespace App.Domain;

public class UserInTeam : BaseEntity
{
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    [Display(Name = nameof(Role), Prompt = nameof(Role), ResourceType = typeof(App.Resources.Domain.UserInTeam))]
    public string Role { get; set; } = default!;  // TODO: TULEB FK??
    
    
    [Display(Name = nameof(Since), Prompt = nameof(Since), ResourceType = typeof(App.Resources.Domain.UserInTeam))]
    public DateTime Since { get; set; }
    
    
    [Display(Name = nameof(Until), Prompt = nameof(Until), ResourceType = typeof(App.Resources.Domain.UserInTeam))]
    public DateTime? Until { get; set; }
    
    
    public Guid UserId { get; set; }
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.UserInTeam))]
    public Person? User { get; set; }
    
    
    public Guid TeamId { get; set; }
    [Display(Name = nameof(Team), Prompt = nameof(Team), ResourceType = typeof(App.Resources.Domain.UserInTeam))]
    public Team? Team { get; set; }
    
    
    public ICollection<Task>? Tasks { get; set; }
    public ICollection<UserInTeamInTask>? UserInTeamInTasks { get; set; }
}