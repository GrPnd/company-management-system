using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Role : BaseEntity
{
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Role))]
    public string Name { get; set; } = default!;
    
    
    public ICollection<UserInRole>? UsersInRoles { get; set; }
}