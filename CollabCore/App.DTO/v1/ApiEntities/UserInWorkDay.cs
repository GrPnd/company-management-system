using Base.Contracts;

namespace App.DTO.v1.ApiEntities;

public class UserInWorkDay : IDomainId
{
    public Guid Id { get; set; }
}