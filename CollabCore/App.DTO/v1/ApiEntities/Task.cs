using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Task : IDomainId
{
    public Guid Id { get; set; }
}