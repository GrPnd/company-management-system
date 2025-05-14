using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class TeamRoleViewModel
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
}