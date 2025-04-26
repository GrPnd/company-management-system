using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Team : IDomainId
{
    public Guid Id { get; set; }
}