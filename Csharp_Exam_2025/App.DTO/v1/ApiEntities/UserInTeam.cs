using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class UserInTeam : IDomainId
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public Guid UserId { get; set; }
}
