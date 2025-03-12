using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Meeting : BaseEntity
{
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Meeting))]
    public string Name { get; set; } = default!;
    
    
    [Display(Name = nameof(IsMandatory), Prompt = nameof(IsMandatory), ResourceType = typeof(App.Resources.Domain.Meeting))]
    public bool IsMandatory { get; set; }
    
    
    [Display(Name = nameof(StartDate), Prompt = nameof(StartDate), ResourceType = typeof(App.Resources.Domain.Meeting))]
    public DateTime StartDate { get; set; }
    
    
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    [Display(Name = nameof(Link), Prompt = nameof(Link), ResourceType = typeof(App.Resources.Domain.Meeting))]
    public string Link { get; set; } = default!;
    
    
    public Guid TeamId { get; set; }
    [Display(Name = nameof(Team), Prompt = nameof(Team), ResourceType = typeof(App.Resources.Domain.Meeting))]
    public Team? Team { get; set; }
}