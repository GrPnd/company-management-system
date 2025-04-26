using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInTeamUOWMapper : IUOWMapper<App.DAL.DTO.UserInTeam, App.Domain.UserInTeam>
{
    private readonly PersonUOWMapper _personUOWMapper = new();
    private readonly TaskUOWMapper _taskUOWMapper = new();
    
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
            User = _personUOWMapper.Map(entity.User),
            TeamId = entity.TeamId,
            Team = null, // prevent circular reference
            Tasks = entity.Tasks?.Select(_taskUOWMapper.Map).ToList()!,
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => new UserInTeamInTask
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                Review = u.Review,
                TaskId = u.TaskId,
                Task = null, // todo ????
                UserInTeamId = u.UserInTeamId,
                UserInTeam = null // todo ????
            }).ToList()
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
            User = _personUOWMapper.Map(entity.User),
            TeamId = entity.TeamId,
            Team = null, // prevent circular reference
            Tasks = entity.Tasks?.Select(_taskUOWMapper.Map).ToList()!,
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => new Domain.UserInTeamInTask
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                Review = u.Review,
                TaskId = u.TaskId,
                Task = null,
                UserInTeamId = u.UserInTeamId,
                UserInTeam = null
            }).ToList()
        };
        
        return res;
    }
}