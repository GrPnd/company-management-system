using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class ScheduleApiMapper : IApiMapper<ApiEntities.Schedule, App.BLL.DTO.Schedule>
{
    public Schedule? Map(BLL.DTO.Schedule? entity)
    {
        if (entity == null) return null;

        return new Schedule()
        {
            Id = entity.Id,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            TeamId = entity.TeamId
        };
    }

    public BLL.DTO.Schedule? Map(Schedule? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Schedule()
        {
            Id = entity.Id,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            TeamId = entity.TeamId
        };

    }
}