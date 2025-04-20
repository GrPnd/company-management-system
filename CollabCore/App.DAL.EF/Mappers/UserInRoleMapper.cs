using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInRoleMapper : IMapper<App.DAL.DTO.UserInRole, App.Domain.UserInRole>
{
    private readonly PersonMapper _personMapper = new();
    private readonly RoleMapper _roleMapper = new();
    
    public UserInRole? Map(Domain.UserInRole? entity)
    {
        if (entity == null) return null;

        var res = new UserInRole()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = _personMapper.Map(entity.User),
            RoleId = entity.RoleId,
            Role = _roleMapper.Map(entity.Role)
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
            User = _personMapper.Map(entity.User),
            RoleId = entity.RoleId,
            Role = _roleMapper.Map(entity.Role)
        };
        
        return res;
    }
}