using Task = App.DTO.v1.ApiEntities.Task;

namespace App.DTO.v1.ApiMappers;

public class TaskApiMapper : IApiMapper<ApiEntities.Task, App.BLL.DTO.Task>
{
    public ApiEntities.Task? Map(BLL.DTO.Task? entity)
    {
        if (entity == null) return null;

        return new ApiEntities.Task()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            AssignedAt = entity.AssignedAt,
            Deadline = entity.Deadline,
            StatusId = entity.StatusId,
            UserInTeamId = entity.UserInTeamId
        };
    }

    public BLL.DTO.Task? Map(ApiEntities.Task? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Task()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            AssignedAt = entity.AssignedAt,
            Deadline = entity.Deadline,
            StatusId = entity.StatusId,
            UserInTeamId = entity.UserInTeamId
        };

    }
}