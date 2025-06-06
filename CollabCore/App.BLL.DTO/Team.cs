﻿using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.BLL.DTO;

public class Team : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Team))]
    public string Name { get; set; } = default!;
    
    
    public Guid DepartmentId { get; set; }
    [Display(Name = nameof(Department), Prompt = nameof(Department), ResourceType = typeof(App.Resources.Domain.Team))]
    public Department? Department { get; set; }
    
    
    public ICollection<UserInTeam>? UsersInTeams { get; init; }
    public ICollection<Meeting>? Meetings { get; init; }
}