using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DAL.DTO;

public class Team : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string TeamName { get; set; } = default!;
    
    public ICollection<UserInTeam> TeamMembers { get; set; } = new List<UserInTeam>();
    
}