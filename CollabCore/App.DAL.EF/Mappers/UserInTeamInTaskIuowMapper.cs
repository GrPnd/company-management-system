using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInTeamInTaskIuowMapper : IUOWMapper<App.DAL.DTO.UserInTeamInTask, App.Domain.UserInTeamInTask>
{
    private readonly TaskIuowMapper _taskIuowMapper = new();
    private readonly UserInTeamIuowMapper _userInTeamIuowMapper = new();
    
    public UserInTeamInTask? Map(Domain.UserInTeamInTask? entity)
    {
        if (entity == null) return null;

        var res = new UserInTeamInTask()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            Review = entity.Review,
            TaskId = entity.TaskId,
            Task = _taskIuowMapper.Map(entity.Task),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = _userInTeamIuowMapper.Map(entity.UserInTeam)
        };
        
        return res;
    }

    public Domain.UserInTeamInTask? Map(UserInTeamInTask? entity)
    {
        if (entity == null) return null;

        var res = new Domain.UserInTeamInTask()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            Review = entity.Review,
            TaskId = entity.TaskId,
            Task = _taskIuowMapper.Map(entity.Task),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = _userInTeamIuowMapper.Map(entity.UserInTeam)
        };
        
        return res;
    }
}