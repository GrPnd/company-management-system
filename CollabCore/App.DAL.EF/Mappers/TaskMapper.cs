using App.DAL.DTO;
using Base.DAL.Contracts;
using Task = App.DAL.DTO.Task;

namespace App.DAL.EF.Mappers;

public class TaskMapper : IMapper<App.DAL.DTO.Task, App.Domain.Task>
{
    private readonly StatusMapper _statusMapper = new();
    private readonly UserInTeamMapper _userInTeamMapper = new();
    private readonly TaskMapper _taskMapper = new();
    
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
            Status = _statusMapper.Map(entity.Status),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = _userInTeamMapper.Map(entity.UserInTeam),
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => new UserInTeamInTask()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                Review = u.Review,
                TaskId = u.TaskId,
                Task = _taskMapper.Map(u.Task),
                UserInTeamId = u.UserInTeamId,
                UserInTeam = _userInTeamMapper.Map(u.UserInTeam)
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
            Status = _statusMapper.Map(entity.Status),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = _userInTeamMapper.Map(entity.UserInTeam),
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => new Domain.UserInTeamInTask()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                Review = u.Review,
                TaskId = u.TaskId,
                Task = _taskMapper.Map(u.Task),
                UserInTeamId = u.UserInTeamId,
                UserInTeam = _userInTeamMapper.Map(u.UserInTeam)
            }).ToList()
        };
        
        return res;
    }
}