using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Status : IDomainId
{
    public Guid Id { get; set; }
}