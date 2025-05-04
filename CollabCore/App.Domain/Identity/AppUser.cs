using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser<AppUserRole>
{
    [MaxLength(128)]
    [MinLength(1)]
    [Required]
    public string FirstName { get; set; } = default!;
    
    [MaxLength(128)]
    [MinLength(1)]
    [Required]
    public string LastName { get; set; } = default!;
    
    
    public ICollection<Person>? Persons { get; set; }
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
}