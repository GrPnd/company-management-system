using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Contracts;
using Base.Domain;

namespace App.Domain;

public class Person : BaseEntity, IDomainUser<Guid, AppUser>
{
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    
    
    
    
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