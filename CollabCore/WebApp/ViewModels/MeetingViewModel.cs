using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class MeetingViewModel
{
    public Meeting Meeting { get; set; } = default!;

    [ValidateNever]
    public SelectList TeamSelectList { get; set; } = default!;
}