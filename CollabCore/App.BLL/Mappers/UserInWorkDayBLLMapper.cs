using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class UserInWorkDayBLLMapper : IBLLMapper<App.BLL.DTO.UserInWorkDay, App.DAL.DTO.UserInWorkDay>
{
    private readonly PersonBLLMapper _personUOWMapper = new();
    private readonly WorkDayBLLMapper _workDayUOWMapper = new();
    public UserInWorkDay? Map(DTO.UserInWorkDay? entity)
    {
        if (entity == null) return null;

        var res = new UserInWorkDay()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = _personUOWMapper.Map(entity.User),
            WorkDayId = entity.WorkDayId,
            WorkDay = _workDayUOWMapper.Map(entity.WorkDay)
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
            User = _personUOWMapper.Map(entity.User),
            WorkDayId = entity.WorkDayId,
            WorkDay = _workDayUOWMapper.Map(entity.WorkDay)
        };
        
        return res;
    }
}