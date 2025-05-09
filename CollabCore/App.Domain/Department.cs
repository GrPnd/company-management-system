using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Department : BaseEntity
{
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Department))]
    public string Name { get; set; } = default!;
    
    public ICollection<Team>? Teams { get; init; }
}