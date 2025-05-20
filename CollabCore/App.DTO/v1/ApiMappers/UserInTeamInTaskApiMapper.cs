using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class UserInTeamInTaskApiMapper : IApiMapper<ApiEntities.UserInTeamInTask, App.BLL.DTO.UserInTeamInTask>
{
    public UserInTeamInTask? Map(BLL.DTO.UserInTeamInTask? entity)
    {
        if (entity == null) return null;

        return new UserInTeamInTask()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            TaskId = entity.TaskId,
            UserInTeamId = entity.UserInTeamId
        };
    }

    public BLL.DTO.UserInTeamInTask? Map(UserInTeamInTask? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.UserInTeamInTask()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            TaskId = entity.TaskId,
            UserInTeamId = entity.UserInTeamId
        };

    }
}