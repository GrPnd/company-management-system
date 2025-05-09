using App.DAL.DTO;
using Base.DAL.Contracts;
using Task = System.Threading.Tasks.Task;

namespace App.DAL.EF.Mappers;

public class UserInTeamInTaskUOWMapper : IUOWMapper<App.DAL.DTO.UserInTeamInTask, App.Domain.UserInTeamInTask>
{
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
            Task = TaskUOWMapper.MapSimple(entity.Task),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = UserInTeamUOWMapper.MapSimple(entity.UserInTeam)
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
            Task = TaskUOWMapper.MapSimple(entity.Task),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = UserInTeamUOWMapper.MapSimple(entity.UserInTeam)
        };
        
        return res;
    }
}