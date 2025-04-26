using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Department : IDomainId
{
    public Guid Id { get; set; }
}