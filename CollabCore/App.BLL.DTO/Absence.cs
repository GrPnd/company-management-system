using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.BLL.DTO;

public class Absence : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Reason), Prompt = nameof(Reason), ResourceType = typeof(App.Resources.Domain.Absence))]
    public string Reason { get; set; } = default!;
    
    
    [Display(Name = nameof(StartDate), Prompt = nameof(StartDate), ResourceType = typeof(App.Resources.Domain.Absence))]
    public DateTime StartDate { get; set; }
    
    
    [Display(Name = nameof(EndDate), Prompt = nameof(EndDate), ResourceType = typeof(App.Resources.Domain.Absence))]
    public DateTime EndDate { get; set; }
    
    
    [Display(Name = nameof(IsApproved), Prompt = nameof(IsApproved), ResourceType = typeof(App.Resources.Domain.Absence))]
    public bool IsApproved { get; set; }
    
    
    public Guid ByUserId { get; set; }
    
    [Display(Name = nameof(ByUser), Prompt = nameof(ByUser), ResourceType = typeof(App.Resources.Domain.Absence))]
    public Person ByUser { get; set; } = default!;
    
    
    public Guid AuthorizedByUserId { get; set; }
    
    [Display(Name = nameof(AuthorizedByUser), Prompt = nameof(AuthorizedByUser), ResourceType = typeof(App.Resources.Domain.Absence))]
    public Person AuthorizedByUser { get; set; } = default!;
}