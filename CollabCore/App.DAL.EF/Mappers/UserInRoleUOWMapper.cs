using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInRoleUOWMapper : IUOWMapper<App.DAL.DTO.UserInRole, App.Domain.UserInRole>
{
    private readonly PersonUOWMapper _personUOWMapper = new();
    private readonly RoleUOWMapper _roleUOWMapper = new();
    
    public UserInRole? Map(Domain.UserInRole? entity)
    {
        if (entity == null) return null;

        var res = new UserInRole()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = _personUOWMapper.Map(entity.User),
            RoleId = entity.RoleId,
            Role = _roleUOWMapper.Map(entity.Role)
        };
        
        return res;
    }

    public Domain.UserInRole? Map(UserInRole? entity)
    {
        if (entity == null) return null;

        var res = new Domain.UserInRole()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = _personUOWMapper.Map(entity.User),
            RoleId = entity.RoleId,
            Role = _roleUOWMapper.Map(entity.Role)
        };
        
        return res;
    }
}