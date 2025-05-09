using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Absence : IDomainId
{
    public Guid Id { get; set; }
    
    public string Reason { get; set; } = default!;
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public bool IsApproved { get; set; }
    
    public Guid ByUserId { get; set; }
    
    public Guid AuthorizedByUserId { get; set; }
}