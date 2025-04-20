using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;

namespace App.DTO.Identity;

public class RegisterInfo
{
    [MaxLength(128)]
    public string Email { get; set; } = default!;
    
    [MaxLength(128)]
    public string Password { get; set; } = default!;
    
    [MaxLength(128)]
    public string Firstname { get; set; } = default!;
    
    [MaxLength(128)]
    public string Lastname { get; set; } = default!;
}