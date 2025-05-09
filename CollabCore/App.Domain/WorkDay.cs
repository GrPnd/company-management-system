﻿using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class WorkDay : BaseEntity
{
    [Display(Name = nameof(Day), Prompt = nameof(Day), ResourceType = typeof(App.Resources.Domain.WorkDay))]
    public DateTime Day { get; set; }
    

    public ICollection<UserInWorkDay> UsersInWorkDay { get; set; } = default!;
}