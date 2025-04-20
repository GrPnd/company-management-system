using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class MeetingMapper : IMapper<App.DAL.DTO.Meeting, App.Domain.Meeting>
{
    private readonly TeamMapper _teamMapper = new();
    
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
            Team = _teamMapper.Map(entity.Team)
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
            Team = _teamMapper.Map(entity.Team)
        };
        
        return res;
    }
}