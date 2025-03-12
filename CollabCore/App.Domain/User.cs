using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace App.Domain;

public class User : IdentityUser
{
    [InverseProperty("FromUser")]
    public ICollection<Message>? FromMessages { get; set; }
    
    [InverseProperty("ToUser")]
    public ICollection<Message>? ToMessages { get; set; }
    
    
    
    [InverseProperty("FromUser")]
    public ICollection<Ticket>? FromTickets { get; set; }
    
    [InverseProperty("ToUser")]
    public ICollection<Ticket>? ToTickets { get; set; }
    
    
    
    [InverseProperty("ByUser")]
    public ICollection<Absence>? ByAbsences { get; set; }
    
    [InverseProperty("AuthorizedByUser")]
    public ICollection<Absence>? AuthorizedByAbsences { get; set; }
    
    
    public ICollection<WorkDay>? WorkDays { get; set; }
    
    public ICollection<UserInTeam>? UserInTeams { get; set; }
    
    public ICollection<Role>? Roles { get; set; }
}