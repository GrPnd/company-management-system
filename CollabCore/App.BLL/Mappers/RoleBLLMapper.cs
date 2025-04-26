using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class RoleBLLMapper : IBLLMapper<App.BLL.DTO.Role, App.DAL.DTO.Role>
{
    private readonly PersonBLLMapper _personUOWMapper = new();
    private readonly RoleBLLMapper _roleUOWMapper = new();
    public Role? Map(DTO.Role? entity)
    {
        if (entity == null) return null;

        var res = new Role()
        {
            Id = entity.Id,
            Name = entity.Name,
            Users = entity.Users?.Select(u => new UserInRole()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                UserId = u.UserId,
                User = _personUOWMapper.Map(u.User),
                RoleId = u.RoleId,
                Role = _roleUOWMapper.Map(u.Role)
            }).ToList()
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
            Users = entity.Users?.Select(u => new DTO.UserInRole()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                UserId = u.UserId,
                User = _personUOWMapper.Map(u.User),
                RoleId = u.RoleId,
                Role = _roleUOWMapper.Map(u.Role)
            }).ToList()
        };
        
        return res;
    }
}