using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class TeamViewModel
{
    public Team Team { get; set; } = default!;

    [ValidateNever]
    public SelectList DepartmentsSelectList { get; set; } = default!;
}