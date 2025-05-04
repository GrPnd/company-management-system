using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class StatusBLLMapper : IBLLMapper<App.BLL.DTO.Status, App.DAL.DTO.Status>
{
    public Status? Map(DTO.Status? entity)
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

    public DTO.Status? Map(Status? entity)
    {
        if (entity == null) return null;

        var res = new DTO.Status()
        {
            Id = entity.Id,
            Name = entity.Name,
            Tasks = entity.Tasks?.Select(t => new App.BLL.DTO.Task()
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