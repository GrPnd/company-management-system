using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class RoleUOWMapper : IUOWMapper<App.DAL.DTO.Role, App.Domain.Role>
{
    private readonly PersonUOWMapper _personUOWMapper = new();
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
                User = _personUOWMapper.Map(u.User),
                RoleId = u.RoleId,
                Role = null
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
                User = _personUOWMapper.Map(u.User),
                RoleId = u.RoleId,
                Role = null
            }).ToList()
        };
        
        return res;
    }
}