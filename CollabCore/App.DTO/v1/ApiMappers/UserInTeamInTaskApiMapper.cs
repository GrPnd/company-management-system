using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class UserInTeamInTaskApiMapper : IApiMapper<ApiEntities.UserInTeamInTask, App.BLL.DTO.UserInTeamInTask>
{
    public UserInTeamInTask? Map(BLL.DTO.UserInTeamInTask? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.UserInTeamInTask? Map(UserInTeamInTask? entity)
    {
        throw new NotImplementedException();
    }
}