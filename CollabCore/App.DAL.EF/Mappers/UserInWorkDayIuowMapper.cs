using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInWorkDayIuowMapper : IUOWMapper<App.DAL.DTO.UserInWorkDay, App.Domain.UserInWorkDay>
{
    private readonly PersonIuowMapper _personIuowMapper = new();
    private readonly WorkDayIuowMapper _workDayIuowMapper = new();
    
    public UserInWorkDay? Map(Domain.UserInWorkDay? entity)
    {
        if (entity == null) return null;

        var res = new UserInWorkDay()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = _personIuowMapper.Map(entity.User),
            WorkDayId = entity.WorkDayId,
            WorkDay = _workDayIuowMapper.Map(entity.WorkDay)
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
            User = _personIuowMapper.Map(entity.User),
            WorkDayId = entity.WorkDayId,
            WorkDay = _workDayIuowMapper.Map(entity.WorkDay)
        };
        
        return res;
    }
}