﻿using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Task : BaseEntity
{
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Task))]
    public string Name { get; set; } = default!;
    
    
    [Display(Name = nameof(Description), Prompt = nameof(Description), ResourceType = typeof(App.Resources.Domain.Task))]
    public string? Description { get; set; }
    
    
    [Display(Name = nameof(AssignedAt), Prompt = nameof(AssignedAt), ResourceType = typeof(App.Resources.Domain.Task))]
    public DateTime AssignedAt { get; set; }
    
    
    [Display(Name = nameof(Deadline), Prompt = nameof(Deadline), ResourceType = typeof(App.Resources.Domain.Task))]
    public DateTime Deadline { get; set; }
    
    
    public Guid StatusId { get; set; }
    [Display(Name = nameof(Status), Prompt = nameof(Status), ResourceType = typeof(App.Resources.Domain.Task))]
    public Status? Status { get; set; }
    
    
    public ICollection<UserInTeamInTask>? UserInTeamInTasks { get; set; }
}