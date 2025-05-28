using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Team : BaseEntity
{
    [MaxLength(128)]
    public string TeamName { get; set; } = default!;
    
    public ICollection<UserInTeam> TeamMembers { get; set; } = new List<UserInTeam>();

}