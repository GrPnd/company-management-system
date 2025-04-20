using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class UserInRoleViewModel
{
    public UserInRole UserInRole { get; set; } = default!;

    [ValidateNever]
    public SelectList RolesSelectList { get; set; } = default!;

    [ValidateNever]
    public SelectList UsersSelectList { get; set; } = default!;
}