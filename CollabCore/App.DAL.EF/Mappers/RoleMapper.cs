using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class RoleMapper : IMapper<App.DAL.DTO.Role, App.Domain.Role>
{
    private readonly PersonMapper _personMapper = new();
    private readonly RoleMapper _roleMapper = new();
    
    public Role? Map(Domain.Role? entity)
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
                User = _personMapper.Map(u.User),
                RoleId = u.RoleId,
                Role = _roleMapper.Map(u.Role)
            }).ToList()
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
            Users = entity.Users?.Select(u => new Domain.UserInRole()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                UserId = u.UserId,
                User = _personMapper.Map(u.User),
                RoleId = u.RoleId,
                Role = _roleMapper.Map(u.Role)
            }).ToList()
        };
        
        return res;
    }
}