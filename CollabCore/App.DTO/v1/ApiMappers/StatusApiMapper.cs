using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class StatusApiMapper : IApiMapper<ApiEntities.Status, App.BLL.DTO.Status>
{
    public Status? Map(BLL.DTO.Status? entity)
    {
        if (entity == null) return null;

        return new Status()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public BLL.DTO.Status? Map(Status? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Status()
        {
            Id = entity.Id,
            Name = entity.Name
        };

    }
}