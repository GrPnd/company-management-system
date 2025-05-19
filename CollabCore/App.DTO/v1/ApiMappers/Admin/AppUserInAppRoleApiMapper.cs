using App.Domain.Identity;
using App.DTO.v1.ApiEntities.Admin;

namespace App.DTO.v1.ApiMappers.Admin;

public class AppUserInAppRoleApiMapper
{
    public static AppUserInAppRole ToDto(AppUserRole entity)
    {
        return new AppUserInAppRole
        {
            Id = entity.Id,
            AppUserId = entity.UserId,
            AppRoleId = entity.RoleId
        };
    }

    public static AppUserRole ToDomain(AppUserInAppRole dto)
    {
        return new AppUserRole
        {
            Id = dto.Id,
            UserId = dto.AppUserId,
            RoleId = dto.AppRoleId
        };
    }
}