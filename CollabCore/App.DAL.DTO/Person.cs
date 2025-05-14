using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Contracts;

namespace App.DAL.DTO;

public class Person : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string PersonName { get; set; } = default!;
    
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
    
    public ICollection<TeamRole>? TeamRoles { get; set; }
}