using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class WorkDayApiMapper : IApiMapper<ApiEntities.WorkDay, App.BLL.DTO.WorkDay>
{
    public WorkDay? Map(BLL.DTO.WorkDay? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.WorkDay? Map(WorkDay? entity)
    {
        throw new NotImplementedException();
    }
}