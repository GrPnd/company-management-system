using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class StatusApiMapper : IApiMapper<ApiEntities.Status, App.BLL.DTO.Status>
{
    public Status? Map(BLL.DTO.Status? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.Status? Map(Status? entity)
    {
        throw new NotImplementedException();
    }
}