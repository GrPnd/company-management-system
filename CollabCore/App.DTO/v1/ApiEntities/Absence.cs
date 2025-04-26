using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Absence : IDomainId
{
    public Guid Id { get; set; }
}