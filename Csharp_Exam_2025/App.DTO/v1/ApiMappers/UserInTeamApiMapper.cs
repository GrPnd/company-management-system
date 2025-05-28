using Base.Contracts;

namespace App.DTO.v1.ApiMappers;

public class UserInTeamApiMapper : IMapper<ApiEntities.UserInTeam, App.DAL.DTO.UserInTeam>
{
    public ApiEntities.UserInTeam? MapTo(App.DAL.DTO.UserInTeam? entity)
    {
        if (entity == null) return null;

        var res = new ApiEntities.UserInTeam()
        {
            Id = entity.Id,
            TeamId = entity.TeamId,
            UserId = entity.UserId,
        };
        return res;

    }

    public App.DAL.DTO.UserInTeam? MapFrom(ApiEntities.UserInTeam? entity)
    {
        if (entity == null) return null;

        var res = new App.DAL.DTO.UserInTeam()
        {
            Id = entity.Id,
            TeamId = entity.TeamId,
            UserId = entity.UserId,
        };
        return res;
    }
}