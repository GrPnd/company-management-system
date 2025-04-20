using App.DAL.DTO;
using Base.DAL.Contracts;
using Task = App.DAL.DTO.Task;

namespace App.DAL.EF.Mappers;

public class TeamMapper : IMapper<App.DAL.DTO.Team, App.Domain.Team>
{
    private readonly DepartmentMapper _departmentMapper = new();
    private readonly UserInTeamMapper _userInTeamMapper = new();
    
    public Team? Map(Domain.Team? entity)
    {
        if (entity == null) return null;

        var res = new Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId,
            Department = _departmentMapper.Map(entity.Department),
            UsersInTeams = entity.UsersInTeams?.Select(_userInTeamMapper.Map).ToList()!,
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
            Department = _departmentMapper.Map(entity.Department),
            UsersInTeams = entity.UsersInTeams?.Select(_userInTeamMapper.Map).ToList()!,
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