using Base.Contracts;

namespace App.DTO.v1.ApiEntities.Admin;

public class AppUserInAppRole : IDomainId
{
    public Guid Id { get; set; }
    public Guid AppUserId { get; set; }
    public Guid AppRoleId { get; set; }
}