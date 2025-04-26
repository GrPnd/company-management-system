using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class UserInRoleApiMapper : IApiMapper<ApiEntities.UserInRole, App.BLL.DTO.UserInRole>
{
    public UserInRole? Map(BLL.DTO.UserInRole? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.UserInRole? Map(UserInRole? entity)
    {
        throw new NotImplementedException();
    }
}