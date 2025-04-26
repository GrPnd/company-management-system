using App.DAL.DTO;
using Base.BLL.Contracts;
using Task = App.DAL.DTO.Task;

namespace App.BLL.Mappers;

public class TaskBLLMapper : IBLLMapper<App.BLL.DTO.Task, App.DAL.DTO.Task>
{
    private readonly StatusBLLMapper _statusUOWMapper = new();
    private readonly UserInTeamBLLMapper _userInTeamUOWMapper = new();
    private readonly TaskBLLMapper _taskUOWMapper = new();
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
            Status = _statusUOWMapper.Map(entity.Status),
            UserInTeamId = entity.UserInTeamId,
            UserInTeam = _userInTeamUOWMapper.Map(entity.UserInTeam),
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => new DTO.UserInTeamInTask()
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