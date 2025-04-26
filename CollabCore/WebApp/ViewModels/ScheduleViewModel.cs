using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class ScheduleViewModel
{
    public Schedule Schedule { get; set; } = default!;

    [ValidateNever]
    public SelectList TeamSelectList { get; set; } = default!;
}