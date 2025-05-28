using App.DAL.DTO;
using Base.Contracts;

namespace App.DAL.EF.Mappers;

public class TeamUOWMapper : IMapper<App.DAL.DTO.Team, App.Domain.Team>
{
    private readonly UserInTeamUOWMapper _userInTeamUOWMapper = new();
    public Team? MapTo(Domain.Team? entity)
    {
        if (entity == null) return null;

        var res = new Team()
        {
            Id = entity.Id,
            TeamName = entity.TeamName,
            TeamMembers = entity.TeamMembers.Select(t => _userInTeamUOWMapper.MapTo(t)).ToList()!
        };
        return res;
    }

    public Domain.Team? MapFrom(Team? entity)
    {
        if (entity == null) return null;
        var res = new Domain.Team()
        {
            Id = entity.Id,
            TeamName = entity.TeamName,
            TeamMembers = entity.TeamMembers.Select(t => _userInTeamUOWMapper.MapFrom(t)).ToList()!
        };
        return res;
    }
    
    public static Team? MapSimple(Domain.Team? entity)
    {
        if (entity == null) return null;

        return new Team()
        {
            Id = entity.Id,
            TeamName = entity.TeamName
        };
    }

    public static Domain.Team? MapSimple(Team? entity)
    {
        if (entity == null) return null;

        return new Domain.Team()
        {
            Id = entity.Id,
            TeamName = entity.TeamName
        };
    }
}
