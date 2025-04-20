using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class UserInWorkDayMapper : IMapper<App.DAL.DTO.UserInWorkDay, App.Domain.UserInWorkDay>
{
    private readonly PersonMapper _personMapper = new();
    private readonly WorkDayMapper _workDayMapper = new();
    
    public UserInWorkDay? Map(Domain.UserInWorkDay? entity)
    {
        if (entity == null) return null;

        var res = new UserInWorkDay()
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = _personMapper.Map(entity.User),
            WorkDayId = entity.WorkDayId,
            WorkDay = _workDayMapper.Map(entity.WorkDay)
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
            User = _personMapper.Map(entity.User),
            WorkDayId = entity.WorkDayId,
            WorkDay = _workDayMapper.Map(entity.WorkDay)
        };
        
        return res;
    }
}