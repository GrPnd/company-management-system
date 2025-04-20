using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInTeamInTaskMapper : IMapper<App.DAL.DTO.UserInTeamInTask, App.Domain.UserInTeamInTask>
{
    private readonly TaskMapper _taskMapper = new();
    private readonly UserInTeamMapper _userInTeamMapper = new();
    
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
            Task = _taskMapper.Map(entity.Task),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = _userInTeamMapper.Map(entity.UserInTeam)
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
            Task = _taskMapper.Map(entity.Task),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = _userInTeamMapper.Map(entity.UserInTeam)
        };
        
        return res;
    }
}