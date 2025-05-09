using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Team : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Name { get; set; } = default!;
    
    public Guid DepartmentId { get; set; }
}