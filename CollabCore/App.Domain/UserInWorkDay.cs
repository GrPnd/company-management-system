using Base.Domain;

namespace App.Domain;

public class UserInWorkDay : BaseEntity
{
    public DateTime SinceDate { get; set; }
    public DateTime ToDate { get; set; }
    
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Guid WorkDayId { get; set; }
    public WorkDay? WorkDay { get; set; }
}