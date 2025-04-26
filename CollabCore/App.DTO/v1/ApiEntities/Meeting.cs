using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Meeting : IDomainId
{
    public Guid Id { get; set; }
}