using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Ticket : IDomainId
{
    public Guid Id { get; set; }
}