using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class UserInWorkDayApiMapper : IApiMapper<ApiEntities.UserInWorkDay, App.BLL.DTO.UserInWorkDay>
{
    public UserInWorkDay? Map(BLL.DTO.UserInWorkDay? entity)
    {
        if (entity == null) return null;

        return new UserInWorkDay()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            WorkDayId = entity.WorkDayId
        };
    }

    public BLL.DTO.UserInWorkDay? Map(UserInWorkDay? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.UserInWorkDay()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            WorkDayId = entity.WorkDayId
        };
    }
}