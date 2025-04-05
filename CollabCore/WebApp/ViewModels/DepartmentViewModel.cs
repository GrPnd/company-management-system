using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;

namespace WebApp.ViewModels;

public class DepartmentViewModel : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }

    
    [DataType(DataType.DateTime)]
    public DateTime? DeletedAt { get; set; }
}