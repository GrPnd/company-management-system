using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class WorkDayApiMapper : IApiMapper<ApiEntities.WorkDay, App.BLL.DTO.WorkDay>
{
    public WorkDay? Map(BLL.DTO.WorkDay? entity)
    {
        if (entity == null) return null;

        return new WorkDay()
        {
            Id = entity.Id,
            Day = entity.Day
        };
    }

    public BLL.DTO.WorkDay? Map(WorkDay? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.WorkDay()
        {
            Id = entity.Id,
            Day = entity.Day
        };
    }
}