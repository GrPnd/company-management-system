using Base.Domain;

namespace App.Domain;

public class Schedule : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
}