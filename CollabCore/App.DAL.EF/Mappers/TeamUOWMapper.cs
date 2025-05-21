using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class TeamUOWMapper : IUOWMapper<App.DAL.DTO.Team, App.Domain.Team>
{
    private readonly UserInTeamUOWMapper _userInTeamUOWMapper = new();
    private readonly MeetingUOWMapper _meetingUOWMapper = new();
    
    public Team? Map(Domain.Team? entity)
    {
        if (entity == null) return null;

        var res = new Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId,
            Department = DepartmentUOWMapper.MapSimple(entity.Department),
            UsersInTeams = entity.UsersInTeams?.Select(u => _userInTeamUOWMapper.Map(u)).ToList()!,
            Meetings = entity.Meetings?.Select(m => _meetingUOWMapper.Map(m)).ToList()!
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
            Department = DepartmentUOWMapper.MapSimple(entity.Department),
            UsersInTeams = entity.UsersInTeams?.Select(u => _userInTeamUOWMapper.Map(u)).ToList()!,
            Meetings = entity.Meetings?.Select(m => _meetingUOWMapper.Map(m)).ToList()!
        };
        
        return res;
    }


    public static Team? MapSimple(Domain.Team entity)
    {
        if (entity == null) return null;

        return new Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId
        };
    }
    
    public static Domain.Team? MapSimple(Team entity)
    {
        if (entity == null) return null;

        return new Domain.Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId
        };
    }
}