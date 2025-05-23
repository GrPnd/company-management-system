﻿using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace WebApp.ViewModels;

public class TeamRoleEditViewModel : IDomainId
{
    public Guid Id { get; set; }
    
    
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
}