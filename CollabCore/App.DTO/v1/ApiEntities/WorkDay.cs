﻿using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class WorkDay : IDomainId
{
    public Guid Id { get; set; }
    
    public DateTime Day { get; set; }
}