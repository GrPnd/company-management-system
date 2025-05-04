using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class WorkDayUOWMapper : IUOWMapper<App.DAL.DTO.WorkDay, App.Domain.WorkDay>
{
    private readonly PersonUOWMapper _personUOWMapper = new();
    
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
                User = null,
                WorkDayId = u.WorkDayId,
                WorkDay = null
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
                User = null,
                WorkDayId = u.WorkDayId,
                WorkDay = null
            }).ToList()
        };
        
        return res;
    }
}