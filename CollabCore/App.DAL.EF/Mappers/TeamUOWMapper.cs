using App.DAL.DTO;
using Base.DAL.Contracts;
using Task = App.DAL.DTO.Task;

namespace App.DAL.EF.Mappers;

public class TeamUOWMapper : IUOWMapper<App.DAL.DTO.Team, App.Domain.Team>
{
    private readonly DepartmentUOWMapper _departmentUOWMapper = new();
    private readonly UserInTeamUOWMapper _userInTeamUOWMapper = new();
    
    public Team? Map(Domain.Team? entity)
    {
        if (entity == null) return null;

        var res = new Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId,
            Department = _departmentUOWMapper.Map(entity.Department),
            UsersInTeams = entity.UsersInTeams?.Select(u => new UserInTeam()
            {
                Id = u.Id,
                Role = u.Role,
                Since = u.Since,
                Until = u.Until,
                UserId = u.UserId,
                User = null,
                TeamId = u.TeamId,
                Team = null,
                Tasks = null,
                UserInTeamInTasks = null
            }).ToList()!,
            Schedules = entity.Schedules?.Select(s => new Schedule()
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                TeamId = s.TeamId,
                Team = null // prevent circular reference
            }).ToList(),
            Meetings = entity.Meetings?.Select(m => new Meeting()
            {
                Id = m.Id,
                Name = m.Name,
                IsMandatory = m.IsMandatory,
                StartDate = m.StartDate,
                Link = m.Link,
                TeamId = m.TeamId,
                Team = null // prevent circular reference
            }).ToList()
        };
        
        return res;
    }

    public Domain.Team? Map(Team? entity)
    { 
        if (entity == null) return null;

        var res = new Domain.Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId,
            Department = _departmentUOWMapper.Map(entity.Department),
            UsersInTeams = entity.UsersInTeams?.Select(u => new Domain.UserInTeam()
            {
                Id = u.Id,
                Role = u.Role,
                Since = u.Since,
                Until = u.Until,
                UserId = u.UserId,
                User = null,
                TeamId = u.TeamId,
                Team = null,
                Tasks = null,
                UserInTeamInTasks = null
            }).ToList()!,
            Schedules = entity.Schedules?.Select(s => new Domain.Schedule()
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                TeamId = s.TeamId,
                Team = null // prevent circular reference
            }).ToList(),
            Meetings = entity.Meetings?.Select(m => new Domain.Meeting()
            {
                Id = m.Id,
                Name = m.Name,
                IsMandatory = m.IsMandatory,
                StartDate = m.StartDate,
                Link = m.Link,
                TeamId = m.TeamId,
                Team = null // prevent circular reference
            }).ToList()
        };
        
        return res;
    }
}