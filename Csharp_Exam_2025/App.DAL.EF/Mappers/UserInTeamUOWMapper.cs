using App.DAL.DTO;
using Base.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInTeamUOWMapper : IMapper<App.DAL.DTO.UserInTeam, App.Domain.UserInTeam>
{
    public UserInTeam? MapTo(Domain.UserInTeam? entity)
    {
        if (entity == null) return null;

        var res = new UserInTeam()
        {
            Id = entity.Id,
            TeamId = entity.TeamId,
            Team = TeamUOWMapper.MapSimple(entity.Team)!,
            UserId = entity.UserId,
        };
        return res;

    }

    public Domain.UserInTeam? MapFrom(UserInTeam? entity)
    {
        if (entity == null) return null;

        var res = new Domain.UserInTeam()
        {
            Id = entity.Id,
            TeamId = entity.TeamId,
            Team = TeamUOWMapper.MapSimple(entity.Team)!,
            UserId = entity.UserId,
        };
        return res;
    }
}