using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class UserInWorkDay : IDomainId
{
    public Guid Id { get; set; }
    
    public DateTime Since { get; set; }
    
    public DateTime Until { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid WorkDayId { get; set; }
}