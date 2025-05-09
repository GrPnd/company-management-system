using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class MeetingApiMapper : IApiMapper<ApiEntities.Meeting, App.BLL.DTO.Meeting>
{
    public Meeting? Map(BLL.DTO.Meeting? entity)
    {
        if (entity == null) return null;

        return new Meeting()
        {
            Id = entity.Id,
            IsMandatory = entity.IsMandatory,
            Link = entity.Link,
            Name = entity.Name,
            StartDate = entity.StartDate,
            TeamId = entity.TeamId
        };
    }

    public BLL.DTO.Meeting? Map(Meeting? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Meeting()
        {
            Id = entity.Id,
            IsMandatory = entity.IsMandatory,
            Link = entity.Link,
            Name = entity.Name,
            StartDate = entity.StartDate,
            TeamId = entity.TeamId
        };
    }
}