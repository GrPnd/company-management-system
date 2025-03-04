using Base.Domain;

namespace App.Domain;

public class Absence : BaseEntity
{
    public string Reason { get; set; } = default!;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public bool IsApproved { get; set; }
    
    public Guid ByUserId { get; set; }
    public User ByUser { get; set; } = default!;
    
    public Guid AuthorizedByUserId { get; set; }
    public User AuthorizedByUser { get; set; } = default!;
}