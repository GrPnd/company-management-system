using Base.Domain;

namespace App.Domain;

public class Message : BaseEntity
{
    public string Text { get; set; } = default!;
    
    public Guid FromUserId { get; set; }
    public User FromUser { get; set; } = default!;
    
    public Guid ToUserId { get; set; }
    public User ToUser { get; set; } = default!;
}