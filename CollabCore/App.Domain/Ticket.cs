using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Ticket : BaseEntity
{
    [MaxLength(128)]
    public string Title { get; set; } = default!;
    
    public string Description { get; set; } = default!;
    
    public Guid FromUserId { get; set; }
    public User FromUser { get; set; } = default!;
    
    public Guid ToUserId { get; set; }
    public User ToUser { get; set; } = default!;
}