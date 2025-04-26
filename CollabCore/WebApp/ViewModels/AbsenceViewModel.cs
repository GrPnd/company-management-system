using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class AbsenceViewModel
{
    public Absence Absence { get; set; } = default!;

    [ValidateNever]
    public SelectList AuthorizedByUserSelectList { get; set; } = default!;

    [ValidateNever]
    public SelectList ByUserSelectList { get; set; } = default!;
}