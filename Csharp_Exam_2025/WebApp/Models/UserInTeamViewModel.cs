using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class UserInTeamViewModel
{
    public UserInTeam UserInTeam { get; set; } = default!;

    [ValidateNever]
    public SelectList UserSelectList { get; set; } = default!;

    [ValidateNever]
    public SelectList TeamSelectList { get; set; } = default!;

}