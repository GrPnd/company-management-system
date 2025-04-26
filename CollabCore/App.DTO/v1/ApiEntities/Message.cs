using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class Message : IDomainId
{
    public Guid Id { get; set; }
}