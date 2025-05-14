using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class TeamRoleUOWMapper : IUOWMapper<App.DAL.DTO.TeamRole, App.Domain.TeamRole>
{
    private readonly UserInTeamUOWMapper _userInTeamUOWMapper = new();
    public TeamRole? Map(Domain.TeamRole? entity)
    {
        if (entity == null) return null;

        var res = new TeamRole()
        {
            Id = entity.Id,
            Name = entity.Name,
            UsersInTeams = entity.UsersInTeams?.Select(u => _userInTeamUOWMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public Domain.TeamRole? Map(TeamRole? entity)
    {
        if (entity == null) return null;

        var res = new Domain.TeamRole()
        {
            Id = entity.Id,
            Name = entity.Name,
            UsersInTeams = entity.UsersInTeams?.Select(u => _userInTeamUOWMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public static TeamRole? MapSimple(Domain.TeamRole? entity)
    {
        if (entity == null) return null;

        return new TeamRole()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    public static Domain.TeamRole? MapSimple(TeamRole? entity)
    {
        if (entity == null) return null;

        return new Domain.TeamRole()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}