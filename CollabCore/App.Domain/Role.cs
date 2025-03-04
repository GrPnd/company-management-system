using Base.Domain;

namespace App.Domain;

public class Role : BaseEntity
{
    public string Name { get; set; } = default!;
    
    public ICollection<UserInRole>? Users { get; set; }
}