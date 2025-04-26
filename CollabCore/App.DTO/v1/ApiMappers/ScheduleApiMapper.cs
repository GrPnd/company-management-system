using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class ScheduleApiMapper : IApiMapper<ApiEntities.Schedule, App.BLL.DTO.Schedule>
{
    public Schedule? Map(BLL.DTO.Schedule? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.Schedule? Map(Schedule? entity)
    {
        throw new NotImplementedException();
    }
}