using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.BLL.DTO;

public class Schedule : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(StartDate), Prompt = nameof(StartDate), ResourceType = typeof(App.Resources.Domain.Schedule))]
    public DateTime StartDate { get; set; }
    
    
    [Display(Name = nameof(EndDate), Prompt = nameof(EndDate), ResourceType = typeof(App.Resources.Domain.Schedule))]
    public DateTime EndDate { get; set; }
    
    
    public Guid TeamId { get; set; }
    
    [Display(Name = nameof(Team), Prompt = nameof(Team), ResourceType = typeof(App.Resources.Domain.Schedule))]
    public Team? Team { get; set; }
}