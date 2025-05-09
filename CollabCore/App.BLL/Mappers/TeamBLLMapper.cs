using App.DAL.DTO;
using App.DAL.EF.Mappers;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class TeamBLLMapper : IBLLMapper<App.BLL.DTO.Team, App.DAL.DTO.Team>
{
    private readonly UserInTeamBLLMapper _userInTeamBLLMapper = new();
    private readonly ScheduleBLLMapper _scheduleBLLMapper = new();
    private readonly MeetingBLLMapper _meetingBLLMapper = new();
    public Team? Map(DTO.Team? entity)
    {
        if (entity == null) return null;

        var res = new Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId,
            Department = DepartmentBLLMapper.MapSimple(entity.Department),
            UsersInTeams = entity.UsersInTeams?.Select(u => _userInTeamBLLMapper.Map(u)).ToList()!,
            Schedules = entity.Schedules?.Select(s => _scheduleBLLMapper.Map(s)).ToList()!,
            Meetings = entity.Meetings?.Select(m => _meetingBLLMapper.Map(m)).ToList()!
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
            Department = DepartmentBLLMapper.MapSimple(entity.Department),
            UsersInTeams = entity.UsersInTeams?.Select(u => _userInTeamBLLMapper.Map(u)).ToList()!,
            Schedules = entity.Schedules?.Select(s => _scheduleBLLMapper.Map(s)).ToList()!,
            Meetings = entity.Meetings?.Select(m => _meetingBLLMapper.Map(m)).ToList()!
        };
        
        return res;
    }
    
    public static Team? MapSimple(DTO.Team entity)
    {
        if (entity == null) return null;

        return new Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId
        };
    }
    
    public static DTO.Team? MapSimple(Team entity)
    {
        if (entity == null) return null;

        return new DTO.Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId
        };
    }
}