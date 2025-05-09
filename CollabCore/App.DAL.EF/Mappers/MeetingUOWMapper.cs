using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class MeetingUOWMapper : IUOWMapper<App.DAL.DTO.Meeting, App.Domain.Meeting>
{
    public Meeting? Map(Domain.Meeting? entity)
    {
        if (entity == null) return null;

        var res = new Meeting()
        {
            Id = entity.Id,
            Name = entity.Name,
            IsMandatory = entity.IsMandatory,
            StartDate = entity.StartDate,
            Link = entity.Link,
            TeamId = entity.TeamId,
            Team = TeamUOWMapper.MapSimple(entity.Team!)
        };
        
        return res;
    }

    public Domain.Meeting? Map(Meeting? entity)
    {
        if (entity == null) return null;

        var res = new Domain.Meeting()
        {
            Id = entity.Id,
            Name = entity.Name,
            IsMandatory = entity.IsMandatory,
            StartDate = entity.StartDate,
            Link = entity.Link,
            TeamId = entity.TeamId,
            Team = TeamUOWMapper.MapSimple(entity.Team!)
        };
        
        return res;
    }
}