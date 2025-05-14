using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class UserInTeamApiMapper : IApiMapper<ApiEntities.UserInTeam, App.BLL.DTO.UserInTeam>
{
    public UserInTeam? Map(BLL.DTO.UserInTeam? entity)
    {
        if (entity == null) return null;

        return new UserInTeam()
        {
            Id = entity.Id,
            TeamRoleId = entity.TeamRoleId,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            TeamId = entity.TeamId
        };
    }

    public BLL.DTO.UserInTeam? Map(UserInTeam? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.UserInTeam()
        {
            Id = entity.Id,
            TeamRoleId = entity.TeamRoleId,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            TeamId = entity.TeamId
        };

    }
}