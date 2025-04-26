using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class UserInTeamViewModel
{
    public UserInTeam UserInTeam { get; set; } = default!;

    [ValidateNever]
    public SelectList TeamSelectList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList UsersSelectList { get; set; } = default!;
}