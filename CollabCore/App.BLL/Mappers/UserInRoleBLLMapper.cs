using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class UserInRoleBLLMapper : IBLLMapper<App.BLL.DTO.UserInRole, App.DAL.DTO.UserInRole>
{
    private readonly PersonBLLMapper _personUOWMapper = new();
    private readonly RoleBLLMapper _roleUOWMapper = new();
    public UserInRole? Map(DTO.UserInRole? entity)
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

    public DTO.UserInRole? Map(UserInRole? entity)
    {
        if (entity == null) return null;

        var res = new DTO.UserInRole()
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