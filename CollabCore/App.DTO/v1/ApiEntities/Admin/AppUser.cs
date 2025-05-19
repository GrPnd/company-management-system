using Base.Contracts;

namespace App.DTO.v1.ApiEntities.Admin;

public class AppUser : IDomainId
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
}