using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class MeetingBLLMapper : IBLLMapper<App.BLL.DTO.Meeting, App.DAL.DTO.Meeting>
{
    public Meeting? Map(DTO.Meeting? entity)
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
            Team = null
        };
        
        return res;
    }

    public DTO.Meeting? Map(Meeting? entity)
    {
        if (entity == null) return null;

        var res = new DTO.Meeting()
        {
            Id = entity.Id,
            Name = entity.Name,
            IsMandatory = entity.IsMandatory,
            StartDate = entity.StartDate,
            Link = entity.Link,
            TeamId = entity.TeamId,
            Team = null
        };
        
        return res;
    }
}