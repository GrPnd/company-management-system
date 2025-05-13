using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class DepartmentViewModel
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
}