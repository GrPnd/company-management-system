using App.DAL.DTO;
using Base.DAL.Contracts;
using Task = App.DAL.DTO.Task;

namespace App.DAL.EF.Mappers;

public class StatusUOWMapper : IUOWMapper<App.DAL.DTO.Status, App.Domain.Status>
{
    private readonly TaskUOWMapper _taskUOWMapper = new();
    
    public Status? Map(Domain.Status? entity)
    {
        if (entity == null) return null;

        var res = new Status()
        {
            Id = entity.Id,
            Name = entity.Name,
            Tasks = entity.Tasks?.Select(t => new App.DAL.DTO.Task()
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                AssignedAt = t.AssignedAt,
                Deadline = t.Deadline,
                StatusId = t.StatusId,
                Status = null,
                UserInTeamId = t.UserInTeamId,
                UserInTeam = null,
                UserInTeamInTasks = null
            }).ToList()!
        };
        
        return res;
    }

    public Domain.Status? Map(Status? entity)
    {
        if (entity == null) return null;

        var res = new Domain.Status()
        {
            Id = entity.Id,
            Name = entity.Name,
            Tasks = entity.Tasks?.Select(t => new Domain.Task()
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                AssignedAt = t.AssignedAt,
                Deadline = t.Deadline,
                StatusId = t.StatusId,
                Status = null,
                UserInTeamId = t.UserInTeamId,
                UserInTeam = null,
                UserInTeamInTasks = null
            }).ToList()!
        };
        
        return res;
    }
}