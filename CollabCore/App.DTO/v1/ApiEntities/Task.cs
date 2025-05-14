using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Task : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Name { get; set; } = default!;
    
    public string? Description { get; set; }
    
    public DateTime AssignedAt { get; set; }
    
    public DateTime Deadline { get; set; }
    
    public Guid StatusId { get; set; }
}