using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class WorkDayBLLMapper : IBLLMapper<App.BLL.DTO.WorkDay, App.DAL.DTO.WorkDay>
{
    private readonly PersonBLLMapper _personUOWMapper = new();
    public WorkDay? Map(DTO.WorkDay? entity)
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
                User = _personUOWMapper.Map(u.User),
                WorkDayId = u.WorkDayId,
                WorkDay = null // prevent circular reference
            }).ToList()
        };
        
        return res;
    }

    public DTO.WorkDay? Map(WorkDay? entity)
    {
        if (entity == null) return null;

        var res = new DTO.WorkDay()
        {
            Id = entity.Id,
            Day = entity.Day,
            UsersInWorkDay = entity.UsersInWorkDay.Select(u => new DTO.UserInWorkDay()
            {
                Id = u.Id,
                Since = u.Since,
                Until = u.Until,
                UserId = u.UserId,
                User = _personUOWMapper.Map(u.User),
                WorkDayId = u.WorkDayId,
                WorkDay = null // prevent circular reference
            }).ToList()
        };
        
        return res;
    }
}