using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class UserInRoleBLLMapper : IBLLMapper<App.BLL.DTO.UserInRole, App.DAL.DTO.UserInRole>
{
    public UserInRole? Map(DTO.UserInRole? entity)
    {
        if (entity == null) return null;

        var res = new UserInRole()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = PersonBLLMapper.MapSimple(entity.User),
            RoleId = entity.RoleId,
            Role = RoleBLLMapper.MapSimple(entity.Role)
        };
        
        return res;
    }

    public DTO.UserInRole? Map(UserInRole? entity)
    {
        if (entity == null) return null;

        var res = new DTO.UserInRole()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = PersonBLLMapper.MapSimple(entity.User),
            RoleId = entity.RoleId,
            Role = RoleBLLMapper.MapSimple(entity.Role)
        };
        
        return res;
    }
}