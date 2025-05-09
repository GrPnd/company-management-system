using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class RoleUOWMapper : IUOWMapper<App.DAL.DTO.Role, App.Domain.Role>
{
    private readonly UserInRoleUOWMapper _userInRoleUOWMapper = new();
    public Role? Map(Domain.Role? entity)
    {
        if (entity == null) return null;

        var res = new Role()
        {
            Id = entity.Id,
            Name = entity.Name,
            UsersInRoles = entity.UsersInRoles?.Select(u => _userInRoleUOWMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public Domain.Role? Map(Role? entity)
    {
        if (entity == null) return null;

        var res = new Domain.Role()
        {
            Id = entity.Id,
            Name = entity.Name,
            UsersInRoles = entity.UsersInRoles?.Select(u => _userInRoleUOWMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public static Role? MapSimple(Domain.Role? entity)
    {
        if (entity == null) return null;

        return new Role()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    public static Domain.Role? MapSimple(Role? entity)
    {
        if (entity == null) return null;

        return new Domain.Role()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}