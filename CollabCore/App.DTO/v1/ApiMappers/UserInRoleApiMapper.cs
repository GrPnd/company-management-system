using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class UserInRoleApiMapper : IApiMapper<ApiEntities.UserInRole, App.BLL.DTO.UserInRole>
{
    public UserInRole? Map(BLL.DTO.UserInRole? entity)
    {
        if (entity == null) return null;

        return new UserInRole()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            RoleId = entity.RoleId
        };
    }

    public BLL.DTO.UserInRole? Map(UserInRole? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.UserInRole()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            RoleId = entity.RoleId
        };

    }
}