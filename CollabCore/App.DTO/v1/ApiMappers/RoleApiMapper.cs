using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class RoleApiMapper : IApiMapper<ApiEntities.Role, App.BLL.DTO.Role>
{
    public Role? Map(BLL.DTO.Role? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.Role? Map(Role? entity)
    {
        throw new NotImplementedException();
    }
}