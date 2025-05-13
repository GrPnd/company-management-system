using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class WorkDay : BaseEntity
{
    [Display(Name = nameof(Day), Prompt = nameof(Day), ResourceType = typeof(App.Resources.Domain.WorkDay))]
    public string Day { get; set; } = default!;
    

    public ICollection<UserInWorkDay>? UsersInWorkDay { get; set; }
}