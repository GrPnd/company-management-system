using App.DAL.DTO;
using Base.DAL.Contracts;
using Task = App.DAL.DTO.Task;

namespace App.DAL.EF.Mappers;

public class TaskUOWMapper : IUOWMapper<App.DAL.DTO.Task, App.Domain.Task>
{
    private readonly StatusUOWMapper _statusUOWMapper = new();
    private readonly UserInTeamUOWMapper _userInTeamUOWMapper = new();
    private readonly TaskUOWMapper _taskUOWMapper = new();
    
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
            Status = _statusUOWMapper.Map(entity.Status),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = _userInTeamUOWMapper.Map(entity.UserInTeam),
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => new UserInTeamInTask()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                Review = u.Review,
                TaskId = u.TaskId,
                Task = _taskUOWMapper.Map(u.Task),
                UserInTeamId = u.UserInTeamId,
                UserInTeam = _userInTeamUOWMapper.Map(u.UserInTeam)
            }).ToList()
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
            Status = _statusUOWMapper.Map(entity.Status),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = _userInTeamUOWMapper.Map(entity.UserInTeam),
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => new Domain.UserInTeamInTask()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                Review = u.Review,
                TaskId = u.TaskId,
                Task = _taskUOWMapper.Map(u.Task),
                UserInTeamId = u.UserInTeamId,
                UserInTeam = _userInTeamUOWMapper.Map(u.UserInTeam)
            }).ToList()
        };
        
        return res;
    }
}