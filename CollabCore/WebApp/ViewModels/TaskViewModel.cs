using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task = App.DAL.DTO.Task;

namespace WebApp.ViewModels;

public class TaskViewModel
{
    public Task Task { get; set; } = default!;

    [ValidateNever]
    public SelectList StatusesSelectList { get; set; } = default!;

    [ValidateNever]
    public SelectList UsersInTeamSelectList { get; set; } = default!;
}