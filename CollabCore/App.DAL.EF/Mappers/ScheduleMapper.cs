using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class ScheduleMapper : IMapper<App.DAL.DTO.Schedule, App.Domain.Schedule>
{
    private readonly TeamMapper _teamMapper = new();
    
    public Schedule? Map(Domain.Schedule? entity)
    {
        if (entity == null) return null;

        var res = new Schedule()
        {
            Id = entity.Id,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            TeamId = entity.TeamId,
            Team = _teamMapper.Map(entity.Team)
        };
        
        return res;
    }

    public Domain.Schedule? Map(Schedule? entity)
    {
        if (entity == null) return null;

        var res = new Domain.Schedule()
        {
            Id = entity.Id,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            TeamId = entity.TeamId,
            Team = _teamMapper.Map(entity.Team)
        };
        
        return res;
    }
}