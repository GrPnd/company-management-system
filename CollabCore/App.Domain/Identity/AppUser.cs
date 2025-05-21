using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;

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
    
    [MaxLength(64)]
    public string? Address { get; set; }
    
    [MaxLength(128)]
    public string? AdditionalInfo { get; set; }
    
    public ICollection<Person>? Persons { get; set; }
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
}