using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInTeamInTaskUOWMapper : IUOWMapper<App.DAL.DTO.UserInTeamInTask, App.Domain.UserInTeamInTask>
{
    private readonly TaskUOWMapper _taskUOWMapper = new();
    private readonly UserInTeamUOWMapper _userInTeamUOWMapper = new();
    
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
            Task = _taskUOWMapper.Map(entity.Task),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = _userInTeamUOWMapper.Map(entity.UserInTeam)
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
            Task = _taskUOWMapper.Map(entity.Task),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = _userInTeamUOWMapper.Map(entity.UserInTeam)
        };
        
        return res;
    }
}