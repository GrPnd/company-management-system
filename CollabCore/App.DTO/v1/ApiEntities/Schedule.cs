using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Schedule : IDomainId
{
    public Guid Id { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public Guid TeamId { get; set; }
}