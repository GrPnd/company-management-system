using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInRoleIuowMapper : IUOWMapper<App.DAL.DTO.UserInRole, App.Domain.UserInRole>
{
    private readonly PersonIuowMapper _personIuowMapper = new();
    private readonly RoleIuowMapper _roleIuowMapper = new();
    
    public UserInRole? Map(Domain.UserInRole? entity)
    {
        if (entity == null) return null;

        var res = new UserInRole()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = _personIuowMapper.Map(entity.User),
            RoleId = entity.RoleId,
            Role = _roleIuowMapper.Map(entity.Role)
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
            User = _personIuowMapper.Map(entity.User),
            RoleId = entity.RoleId,
            Role = _roleIuowMapper.Map(entity.Role)
        };
        
        return res;
    }
}