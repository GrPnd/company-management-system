using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class UserInWorkDay : BaseEntity
{
    [Display(Name = nameof(Since), Prompt = nameof(Since), ResourceType = typeof(App.Resources.Domain.UserInWorkDay))]
    public DateTime Since { get; set; }
    
    
    [Display(Name = nameof(Until), Prompt = nameof(Until), ResourceType = typeof(App.Resources.Domain.UserInWorkDay))]
    public DateTime Until { get; set; }
    
    
    public Guid UserId { get; set; }
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.UserInWorkDay))]
    public User? User { get; set; }
    
    
    public Guid WorkDayId { get; set; }
    [Display(Name = nameof(WorkDay), Prompt = nameof(WorkDay), ResourceType = typeof(App.Resources.Domain.UserInWorkDay))]
    public WorkDay? WorkDay { get; set; }
}