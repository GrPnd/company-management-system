using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Ticket : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Title { get; set; } = default!;
    
    public string Description { get; set; } = default!;
    
    public Guid FromUserId { get; set; }
    
    public Guid ToUserId { get; set; }
}