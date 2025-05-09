using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class UserInWorkDayBLLMapper : IBLLMapper<App.BLL.DTO.UserInWorkDay, App.DAL.DTO.UserInWorkDay>
{
    public UserInWorkDay? Map(DTO.UserInWorkDay? entity)
    {
        if (entity == null) return null;

        var res = new UserInWorkDay()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = PersonBLLMapper.MapSimple(entity.User),
            WorkDayId = entity.WorkDayId,
            WorkDay = WorkDayBLLMapper.MapSimple(entity.WorkDay),
        };
        
        return res;
    }

    public DTO.UserInWorkDay? Map(UserInWorkDay? entity)
    {
        if (entity == null) return null;

        var res = new DTO.UserInWorkDay()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = PersonBLLMapper.MapSimple(entity.User),
            WorkDayId = entity.WorkDayId,
            WorkDay = WorkDayBLLMapper.MapSimple(entity.WorkDay),
        };
        
        return res;
    }
}