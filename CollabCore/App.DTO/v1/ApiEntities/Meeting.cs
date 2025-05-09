using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Meeting : IDomainId
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public bool IsMandatory { get; set; }
    
    public DateTime StartDate { get; set; }
    
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Link { get; set; } = default!;
    
    public Guid TeamId { get; set; }
}