using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class UserInTeamInTaskBLLMapper : IBLLMapper<App.BLL.DTO.UserInTeamInTask, App.DAL.DTO.UserInTeamInTask>
{
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
            Task = TaskBLLMapper.MapSimple(entity.Task),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = UserInTeamBLLMapper.MapSimple(entity.UserInTeam)
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
            Task = TaskBLLMapper.MapSimple(entity.Task),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = UserInTeamBLLMapper.MapSimple(entity.UserInTeam)
        };
        
        return res;
    }
}