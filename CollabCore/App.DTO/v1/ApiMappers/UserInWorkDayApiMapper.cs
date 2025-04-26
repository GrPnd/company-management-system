using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class UserInWorkDayApiMapper : IApiMapper<ApiEntities.UserInWorkDay, App.BLL.DTO.UserInWorkDay>
{
    public UserInWorkDay? Map(BLL.DTO.UserInWorkDay? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.UserInWorkDay? Map(UserInWorkDay? entity)
    {
        throw new NotImplementedException();
    }
}