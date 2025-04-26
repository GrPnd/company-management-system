using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.BLL.DTO;

public class UserInTeamInTask : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Since), Prompt = nameof(Since), ResourceType = typeof(App.Resources.Domain.UserInTeamInTask))]
    public DateTime Since { get; set; }
    
    
    [Display(Name = nameof(Until), Prompt = nameof(Until), ResourceType = typeof(App.Resources.Domain.UserInTeamInTask))]
    public DateTime? Until { get; set; }
    

    [Display(Name = nameof(Review), Prompt = nameof(Review), ResourceType = typeof(App.Resources.Domain.UserInTeamInTask))]
    public string? Review { get; set; }
    
    
    public Guid TaskId { get; set; }
    [Display(Name = nameof(Task), Prompt = nameof(Task), ResourceType = typeof(App.Resources.Domain.UserInTeamInTask))]
    public Task? Task { get; set; }
    
    
    public Guid UserInTeamId { get; set; }
    [Display(Name = nameof(UserInTeam), Prompt = nameof(UserInTeam), ResourceType = typeof(App.Resources.Domain.UserInTeamInTask))]
    public UserInTeam? UserInTeam { get; set; }
}