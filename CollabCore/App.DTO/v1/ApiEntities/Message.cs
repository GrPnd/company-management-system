using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Message : IDomainId
{
    public Guid Id { get; set; }
    
    public string Text { get; set; } = default!;
    
    public Guid FromUserId { get; set; }
    
    public Guid ToUserId { get; set; }
}