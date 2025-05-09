using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class RoleBLLMapper : IBLLMapper<App.BLL.DTO.Role, App.DAL.DTO.Role>
{
    private readonly UserInRoleBLLMapper _userInRoleBLLMapper = new();
    public Role? Map(DTO.Role? entity)
    {
        if (entity == null) return null;

        var res = new Role()
        {
            Id = entity.Id,
            Name = entity.Name,
            UsersInRoles = entity.UsersInRoles?.Select(u => _userInRoleBLLMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public DTO.Role? Map(Role? entity)
    {
        if (entity == null) return null;

        var res = new DTO.Role()
        {
            Id = entity.Id,
            Name = entity.Name,
            UsersInRoles = entity.UsersInRoles?.Select(u => _userInRoleBLLMapper.Map(u)).ToList()!
        };
        
        return res;
    }
    
    public static Role? MapSimple(DTO.Role? entity)
    {
        if (entity == null) return null;

        return new Role()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    public static DTO.Role? MapSimple(Role? entity)
    {
        if (entity == null) return null;

        return new DTO.Role()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}