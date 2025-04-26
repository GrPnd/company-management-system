using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Schedule : IDomainId
{
    public Guid Id { get; set; }
}