using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class UserInTeamInTaskViewModel
{
    public UserInTeamInTask UserInTeamInTask { get; set; } = default!;

    [ValidateNever]
    public SelectList TaskSelectList { get; set; } = default!;

    [ValidateNever]
    public SelectList UsersInTeamSelectList { get; set; } = default!;

}