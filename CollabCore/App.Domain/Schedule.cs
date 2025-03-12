using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Schedule : BaseEntity
{
    [Display(Name = nameof(StartDate), Prompt = nameof(StartDate), ResourceType = typeof(App.Resources.Domain.Schedule))]
    public DateTime StartDate { get; set; }
    
    
    [Display(Name = nameof(EndDate), Prompt = nameof(EndDate), ResourceType = typeof(App.Resources.Domain.Schedule))]
    public DateTime EndDate { get; set; }
    
    
    public Guid TeamId { get; set; }
    
    [Display(Name = nameof(Team), Prompt = nameof(Team), ResourceType = typeof(App.Resources.Domain.Schedule))]
    public Team? Team { get; set; }
    
    
    [Display(Name = nameof(CreatedAt), Prompt = nameof(CreatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; init; }
    
    
    [Display(Name = nameof(DeletedAt), Prompt = nameof(DeletedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime? DeletedAt { get; init; }
}