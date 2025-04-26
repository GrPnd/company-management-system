using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class WorkDayIuowMapper : IUOWMapper<App.DAL.DTO.WorkDay, App.Domain.WorkDay>
{
    private readonly PersonIuowMapper _personIuowMapper = new();
    
    public WorkDay? Map(Domain.WorkDay? entity)
    {
        if (entity == null) return null;

        var res = new WorkDay()
        {
            Id = entity.Id,
            Day = entity.Day,
            UsersInWorkDay = entity.UsersInWorkDay.Select(u => new UserInWorkDay()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                UserId = u.UserId,
                User = _personIuowMapper.Map(u.User),
                WorkDayId = u.WorkDayId,
                WorkDay = null // prevent circular reference
            }).ToList()
        };
        
        return res;
    }

    public Domain.WorkDay? Map(WorkDay? entity)
    {
        if (entity == null) return null;

        var res = new Domain.WorkDay()
        {
            Id = entity.Id,
            Day = entity.Day,
            UsersInWorkDay = entity.UsersInWorkDay.Select(u => new Domain.UserInWorkDay()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                UserId = u.UserId,
                User = _personIuowMapper.Map(u.User),
                WorkDayId = u.WorkDayId,
                WorkDay = null // prevent circular reference
            }).ToList()
        };
        
        return res;
    }
}