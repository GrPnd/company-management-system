using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class ScheduleUOWMapper : IUOWMapper<App.DAL.DTO.Schedule, App.Domain.Schedule>
{
    public Schedule? Map(Domain.Schedule? entity)
    {
        if (entity == null) return null;

        var res = new Schedule()
        {
            Id = entity.Id,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            TeamId = entity.TeamId,
            Team = TeamUOWMapper.MapSimple(entity.Team!)
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
            Team = TeamUOWMapper.MapSimple(entity.Team!)
        };
        
        return res;
    }
}