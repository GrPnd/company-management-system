using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.ViewModels;

public class DepartmentViewModel : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
}