using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Message : BaseEntity
{
    [Display(Name = nameof(Text), Prompt = nameof(Text), ResourceType = typeof(App.Resources.Domain.Message))]
    public string Text { get; set; } = default!;
    
    
    public Guid FromUserId { get; set; }
    [Display(Name = nameof(FromUser), Prompt = nameof(FromUser), ResourceType = typeof(App.Resources.Domain.Message))]
    public User FromUser { get; set; } = default!;
    
    
    public Guid ToUserId { get; set; }
    [Display(Name = nameof(ToUser), Prompt = nameof(ToUser), ResourceType = typeof(App.Resources.Domain.Message))]
    public User ToUser { get; set; } = default!;
    
    
    [Display(Name = nameof(CreatedAt), Prompt = nameof(CreatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; init; }
    
    
    [Display(Name = nameof(DeletedAt), Prompt = nameof(DeletedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime? DeletedAt { get; init; }
}