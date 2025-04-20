using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DAL.DTO;

public class Message : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Text), Prompt = nameof(Text), ResourceType = typeof(App.Resources.Domain.Message))]
    public string Text { get; set; } = default!;
    
    
    public Guid FromUserId { get; set; }
    [Display(Name = nameof(FromUser), Prompt = nameof(FromUser), ResourceType = typeof(App.Resources.Domain.Message))]
    public Person FromUser { get; set; } = default!;
    
    
    public Guid ToUserId { get; set; }
    [Display(Name = nameof(ToUser), Prompt = nameof(ToUser), ResourceType = typeof(App.Resources.Domain.Message))]
    public Person ToUser { get; set; } = default!;
}