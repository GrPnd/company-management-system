using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.BLL.DTO;

public class Department : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Department))]
    public string Name { get; set; } = default!;
    
    public ICollection<Team>? Teams { get; init; }
}