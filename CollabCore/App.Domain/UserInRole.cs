using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class UserInRole : BaseEntity
{
    [Display(Name = nameof(Since), Prompt = nameof(Since), ResourceType = typeof(App.Resources.Domain.UserInRole))]
    public DateTime Since { get; set; }
    
    
    [Display(Name = nameof(Until), Prompt = nameof(Until), ResourceType = typeof(App.Resources.Domain.UserInRole))]
    public DateTime Until { get; set; }
    
    
    public Guid UserId { get; set; }
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.UserInRole))]
    public User? User { get; set; }
    
    
    public Guid RoleId { get; set; }
    [Display(Name = nameof(Role), Prompt = nameof(Role), ResourceType = typeof(App.Resources.Domain.UserInRole))]
    public Role? Role { get; set; }
    
}