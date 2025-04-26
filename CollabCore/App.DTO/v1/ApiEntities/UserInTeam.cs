using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class UserInTeam : IDomainId
{
    public Guid Id { get; set; }
}