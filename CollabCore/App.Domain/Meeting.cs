using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Meeting : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public string Text { get; set; } = default!;
    
    [MaxLength(128)]
    public string Link { get; set; } = default!;
    
    public Guid TeamId { get; set; }
    public Team? Team { get; set; } = default!;
}