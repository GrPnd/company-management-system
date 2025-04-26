using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class UserInRole : IDomainId
{
    public Guid Id { get; set; }
}