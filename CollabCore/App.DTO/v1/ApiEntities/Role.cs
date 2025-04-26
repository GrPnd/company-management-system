using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Role : IDomainId
{
    public Guid Id { get; set; }
}