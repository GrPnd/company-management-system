using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Ticket : BaseEntity
{
    [MaxLength(128, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    [Display(Name = nameof(Title), Prompt = nameof(Title), ResourceType = typeof(App.Resources.Domain.Ticket))]
    public string Title { get; set; } = default!;
    
    
    [Display(Name = nameof(Description), Prompt = nameof(Description), ResourceType = typeof(App.Resources.Domain.Ticket))]
    public string Description { get; set; } = default!;
    
    
    public Guid FromUserId { get; set; }
    [Display(Name = nameof(FromUser), Prompt = nameof(FromUser), ResourceType = typeof(App.Resources.Domain.Ticket))]
    public Person FromUser { get; set; } = default!;
    
    
    public Guid ToUserId { get; set; }
    [Display(Name = nameof(ToUser), Prompt = nameof(ToUser), ResourceType = typeof(App.Resources.Domain.Ticket))]
    public Person ToUser { get; set; } = default!;
    
    
    [Display(Name = nameof(CreatedAt), Prompt = nameof(CreatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; init; }
    
    
    [Display(Name = nameof(DeletedAt), Prompt = nameof(DeletedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime? DeletedAt { get; init; }
}