﻿using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Team : BaseEntity
{
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Team))]
    public string Name { get; set; } = default!;
    
    
    public Guid DepartmentId { get; set; }
    [Display(Name = nameof(Department), Prompt = nameof(Department), ResourceType = typeof(App.Resources.Domain.Team))]
    public Department? Department { get; set; }
    
    
    public ICollection<UserInTeam>? Users { get; init; }
    public ICollection<Schedule>? Schedules { get; init; }
    public ICollection<Meeting>? Meetings { get; init; }
    
    
    [Display(Name = nameof(CreatedAt), Prompt = nameof(CreatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; set; }
    
    
    [Display(Name = nameof(DeletedAt), Prompt = nameof(DeletedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime? DeletedAt { get; set; }
}