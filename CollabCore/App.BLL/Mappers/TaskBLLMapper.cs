using App.DAL.DTO;
using Base.BLL.Contracts;
using Task = App.DAL.DTO.Task;

namespace App.BLL.Mappers;

public class TaskBLLMapper : IBLLMapper<App.BLL.DTO.Task, App.DAL.DTO.Task>
{
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
            Status = new Status()
            {
                Id = entity.StatusId,
                Name = entity.Name,
                Tasks = null
            },
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = null,
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => new UserInTeamInTask()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                Review = u.Review,
                TaskId = u.TaskId,
                Task = null,
                UserInTeamId = u.UserInTeamId,
                UserInTeam = null
            }).ToList()
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
            Status = new DTO.Status()
            {
                Id = entity.StatusId,
                Name = entity.Name,
                Tasks = null
            },
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = null,
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => new DTO.UserInTeamInTask()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                Review = u.Review,
                TaskId = u.TaskId,
                Task = null,
                UserInTeamId = u.UserInTeamId,
                UserInTeam = null
            }).ToList()
        };
        
        return res;
    }
}