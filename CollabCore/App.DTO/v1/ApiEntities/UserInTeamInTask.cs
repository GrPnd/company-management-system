using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class UserInTeamInTask : IDomainId
{
    public Guid Id { get; set; }
}