using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserInTeam : BaseEntityUser<AppUser>
{
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
}
