using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class UserInTeamBLLMapper : IBLLMapper<App.BLL.DTO.UserInTeam, App.DAL.DTO.UserInTeam>
{
    private readonly PersonBLLMapper _personUOWMapper = new();
    private readonly TaskBLLMapper _taskUOWMapper = new();
    public UserInTeam? Map(DTO.UserInTeam? entity)
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
            Tasks = null,
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

    public DTO.UserInTeam? Map(UserInTeam? entity)
    {
        if (entity == null) return null;

        var res = new DTO.UserInTeam()
        {
            Id = entity.Id,
            Role = entity.Role,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = _personUOWMapper.Map(entity.User),
            TeamId = entity.TeamId,
            Team = null, // prevent circular reference
            Tasks = null,
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => new DTO.UserInTeamInTask
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