using App.DAL.DTO;
using App.DAL.EF.Mappers;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class TeamBLLMapper : IBLLMapper<App.BLL.DTO.Team, App.DAL.DTO.Team>
{
    private readonly DepartmentBLLMapper _departmentUOWMapper = new();
    private readonly UserInTeamBLLMapper _userInTeamUOWMapper = new();
    private readonly ScheduleBLLMapper _scheduleUOWMapper = new();
    public Team? Map(DTO.Team? entity)
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

    public DTO.Team? Map(Team? entity)
    {
        if (entity == null) return null;

        var res = new DTO.Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId,
            Department = _departmentUOWMapper.Map(entity.Department),
            UsersInTeams = entity.UsersInTeams?.Select(u => new DTO.UserInTeam()
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
            Schedules = entity.Schedules?.Select(s => new DTO.Schedule()
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                TeamId = s.TeamId,
                Team = null // prevent circular reference
            }).ToList(),
            Meetings = entity.Meetings?.Select(m => new DTO.Meeting()
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