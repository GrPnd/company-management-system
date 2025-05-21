using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.BLL.DTO;

public class Status : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Status))]
    public string Name { get; set; } = default!;
    
    
    public ICollection<Task>? Tasks { get; set; }
}