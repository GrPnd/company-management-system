using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>
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
}