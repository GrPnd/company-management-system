﻿using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class UserInTeamInTask : BaseEntity
{
    [Display(Name = nameof(Since), Prompt = nameof(Since), ResourceType = typeof(App.Resources.Domain.UserInTeamInTask))]
    public DateTime Since { get; set; }
    
    
    [Display(Name = nameof(Until), Prompt = nameof(Until), ResourceType = typeof(App.Resources.Domain.UserInTeamInTask))]
    public DateTime? Until { get; set; }
    
    
    public Guid TaskId { get; set; }
    [Display(Name = nameof(Task), Prompt = nameof(Task), ResourceType = typeof(App.Resources.Domain.UserInTeamInTask))]
    public Task? Task { get; set; }
    
    
    public Guid UserInTeamId { get; set; }
    [Display(Name = nameof(UserInTeam), Prompt = nameof(UserInTeam), ResourceType = typeof(App.Resources.Domain.UserInTeamInTask))]
    public UserInTeam? UserInTeam { get; set; }
}