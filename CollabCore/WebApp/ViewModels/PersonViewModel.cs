using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace WebApp.ViewModels;

public class PersonViewModel
{
    public Person Person { get; set; } = default!;
    
    [ValidateNever]
    public SelectList UsersSelectList { get; set; } = default!;
}