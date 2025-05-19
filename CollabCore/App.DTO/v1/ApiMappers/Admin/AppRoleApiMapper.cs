namespace App.DTO.v1.ApiMappers.Admin;

public class AppRoleApiMapper
{
    public static App.DTO.v1.ApiEntities.Admin.AppRole ToDto(App.Domain.Identity.AppRole role)
    {
        return new App.DTO.v1.ApiEntities.Admin.AppRole
        {
            Id = role.Id,
            RoleName = role.Name
        };
    }

    public static App.Domain.Identity.AppRole ToDomain(App.DTO.v1.ApiEntities.Admin.AppRole dto)
    {
        return new App.Domain.Identity.AppRole
        {
            Id = dto.Id,
            Name = dto.RoleName,
            NormalizedName = dto.RoleName.ToUpperInvariant()
        };
    }
}