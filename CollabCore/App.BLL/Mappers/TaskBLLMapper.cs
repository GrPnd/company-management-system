using App.DAL.DTO;
using Base.BLL.Contracts;
using Task = App.DAL.DTO.Task;

namespace App.BLL.Mappers;

public class TaskBLLMapper : IBLLMapper<App.BLL.DTO.Task, App.DAL.DTO.Task>
{
    private readonly UserInTeamInTaskBLLMapper _userInTeamInTaskBLLMapper = new();
    public Task? Map(DTO.Task? entity)
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
            Status = StatusBLLMapper.MapSimple(entity.Status),
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => _userInTeamInTaskBLLMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public DTO.Task? Map(Task? entity)
    {
        if (entity == null) return null;

        var res = new DTO.Task()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            AssignedAt = entity.AssignedAt,
            Deadline = entity.Deadline,
            StatusId = entity.StatusId,
            Status = StatusBLLMapper.MapSimple(entity.Status),
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => _userInTeamInTaskBLLMapper.Map(u)).ToList()!
        };
        
        return res;
    }
    
    public static Task? MapSimple(DTO.Task? entity)
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
    
    public static DTO.Task? MapSimple(Task? entity)
    {
        if (entity == null) return null;

        return new DTO.Task()
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