using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInWorkDayUOWMapper : IUOWMapper<App.DAL.DTO.UserInWorkDay, App.Domain.UserInWorkDay>
{
    private readonly PersonUOWMapper _personUOWMapper = new();
    private readonly WorkDayUOWMapper _workDayUOWMapper = new();
    
    public UserInWorkDay? Map(Domain.UserInWorkDay? entity)
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

    public Domain.UserInWorkDay? Map(UserInWorkDay? entity)
    {
        if (entity == null) return null;

        var res = new Domain.UserInWorkDay()
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