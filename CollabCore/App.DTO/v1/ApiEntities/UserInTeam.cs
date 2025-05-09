using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class UserInTeam : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Role { get; set; } = default!;  // TODO: TULEB FK??
    
    public DateTime Since { get; set; }
    
    public DateTime? Until { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid TeamId { get; set; }
}