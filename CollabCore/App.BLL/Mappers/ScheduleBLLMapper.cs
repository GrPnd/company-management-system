using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class ScheduleBLLMapper : IBLLMapper<App.BLL.DTO.Schedule, App.DAL.DTO.Schedule>
{
    public Schedule? Map(DTO.Schedule? entity)
    {
        if (entity == null) return null;

        var res = new Schedule()
        {
            Id = entity.Id,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            TeamId = entity.TeamId,
            Team = null
        };
        
        return res;
    }

    public DTO.Schedule? Map(Schedule? entity)
    {
        if (entity == null) return null;

        var res = new DTO.Schedule()
        {
            Id = entity.Id,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            TeamId = entity.TeamId,
            Team = null
        };
        
        return res;
    }
}