using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DAL.DTO;

public class WorkDay : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Day), Prompt = nameof(Day), ResourceType = typeof(App.Resources.Domain.WorkDay))]
    public string Day { get; set; } = default!;
    

    public ICollection<UserInWorkDay>? UsersInWorkDay { get; set; }
}