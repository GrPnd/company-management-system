using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class UserInWorkDayViewModel
{
    public UserInWorkDay UserInWorkDay { get; set; } = default!;
    
    [ValidateNever]
    public SelectList UsersSelectList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList WorkDaysSelectList { get; set; } = default!;
}