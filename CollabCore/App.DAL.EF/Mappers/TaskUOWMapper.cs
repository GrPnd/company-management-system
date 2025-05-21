using Base.DAL.Contracts;
using Task = App.DAL.DTO.Task;

namespace App.DAL.EF.Mappers;

public class TaskUOWMapper : IUOWMapper<App.DAL.DTO.Task, App.Domain.Task>
{
    private readonly UserInTeamInTaskUOWMapper _userInTeamInTaskUOWMapper = new();
    public Task? Map(Domain.Task? entity)
    {
        if (entity == null) return null;

        var res = new Task()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            AssignedAt = entity.AssignedAt,
            Deadline = entity.Deadline,
            StatusId = entity.StatusId,
            Status = StatusUOWMapper.MapSimple(entity.Status),
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => _userInTeamInTaskUOWMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public Domain.Task? Map(Task? entity)
    {
        if (entity == null) return null;

        var res = new Domain.Task()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            AssignedAt = entity.AssignedAt,
            Deadline = entity.Deadline,
            StatusId = entity.StatusId,
            Status = StatusUOWMapper.MapSimple(entity.Status),
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => _userInTeamInTaskUOWMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public static Task? MapSimple(Domain.Task? entity)
    {
        if (entity == null) return null;

        return new Task()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            AssignedAt = entity.AssignedAt,
            Deadline = entity.Deadline,
            StatusId = entity.StatusId
        };
    }
    
    public static Domain.Task? MapSimple(Task? entity)
    {
        if (entity == null) return null;

        return new Domain.Task()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            AssignedAt = entity.AssignedAt,
            Deadline = entity.Deadline,
            StatusId = entity.StatusId
        };
    }
}