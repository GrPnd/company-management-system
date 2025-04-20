using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.ViewModels;

public class RoleViewModel : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
}