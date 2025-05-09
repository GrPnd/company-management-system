using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class RoleApiMapper : IApiMapper<ApiEntities.Role, App.BLL.DTO.Role>
{
    public Role? Map(BLL.DTO.Role? entity)
    {
        if (entity == null) return null;

        return new Role()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public BLL.DTO.Role? Map(Role? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Role()
        {
            Id = entity.Id,
            Name = entity.Name
        };

    }
}