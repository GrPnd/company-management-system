using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class UserInTeamApiMapper : IApiMapper<ApiEntities.UserInTeam, App.BLL.DTO.UserInTeam>
{
    public UserInTeam? Map(BLL.DTO.UserInTeam? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.UserInTeam? Map(UserInTeam? entity)
    {
        throw new NotImplementedException();
    }
}