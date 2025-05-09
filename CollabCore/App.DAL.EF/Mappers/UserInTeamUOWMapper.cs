using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInTeamUOWMapper : IUOWMapper<App.DAL.DTO.UserInTeam, App.Domain.UserInTeam>
{
    private readonly TaskUOWMapper _taskUOWMapper = new();
    private readonly UserInTeamInTaskUOWMapper _userInTeamInTaskUOWMapper = new();
    
    public UserInTeam? Map(Domain.UserInTeam? entity)
    {
        if (entity == null) return null;

        var res = new UserInTeam()
        {
            Id = entity.Id,
            Role = entity.Role,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = PersonUOWMapper.MapSimple(entity.User),
            TeamId = entity.TeamId,
            Team = TeamUOWMapper.MapSimple(entity.Team!),
            Tasks = entity.Tasks?.Select(t => _taskUOWMapper.Map(t)).ToList()!,
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => _userInTeamInTaskUOWMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public Domain.UserInTeam? Map(UserInTeam? entity)
    {
        if (entity == null) return null;

        var res = new Domain.UserInTeam()
        {
            Id = entity.Id,
            Role = entity.Role,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = PersonUOWMapper.MapSimple(entity.User),
            TeamId = entity.TeamId,
            Team = TeamUOWMapper.MapSimple(entity.Team!),
            Tasks = entity.Tasks?.Select(t => _taskUOWMapper.Map(t)).ToList()!,
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => _userInTeamInTaskUOWMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public static UserInTeam? MapSimple(Domain.UserInTeam? entity)
    {
        if (entity == null) return null;

        return new UserInTeam()
        {
            Id = entity.Id,
            Role = entity.Role,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            TeamId = entity.TeamId
        };
    }
    
    public static Domain.UserInTeam? MapSimple(UserInTeam? entity)
    {
        if (entity == null) return null;

        return new Domain.UserInTeam()
        {
            Id = entity.Id,
            Role = entity.Role,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            TeamId = entity.TeamId
        };
    }
}