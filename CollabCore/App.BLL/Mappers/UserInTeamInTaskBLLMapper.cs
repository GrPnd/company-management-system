using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class UserInTeamInTaskBLLMapper : IBLLMapper<App.BLL.DTO.UserInTeamInTask, App.DAL.DTO.UserInTeamInTask>
{
    private readonly TaskBLLMapper _taskUOWMapper = new();
    private readonly UserInTeamBLLMapper _userInTeamUOWMapper = new();
    public UserInTeamInTask? Map(DTO.UserInTeamInTask? entity)
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

    public DTO.UserInTeamInTask? Map(UserInTeamInTask? entity)
    {
        if (entity == null) return null;

        var res = new DTO.UserInTeamInTask()
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