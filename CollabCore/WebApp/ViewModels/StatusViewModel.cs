using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class StatusViewModel
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
}