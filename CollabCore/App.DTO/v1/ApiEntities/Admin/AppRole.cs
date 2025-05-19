using Base.Contracts;

namespace App.DTO.v1.ApiEntities.Admin;

public class AppRole : IDomainId
{
    public Guid Id { get; set; }
    public string RoleName { get; set; } = default!;
}