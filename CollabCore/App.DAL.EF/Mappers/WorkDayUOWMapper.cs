using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class WorkDayUOWMapper : IUOWMapper<App.DAL.DTO.WorkDay, App.Domain.WorkDay>
{
    private readonly UserInWorkDayUOWMapper _userInWorkDayUOWMapper = new();
    
    public WorkDay? Map(Domain.WorkDay? entity)
    {
        if (entity == null) return null;

        var res = new WorkDay()
        {
            Id = entity.Id,
            Day = entity.Day,
            UsersInWorkDay = entity.UsersInWorkDay.Select(u => _userInWorkDayUOWMapper.Map(u)).ToList()!
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
            UsersInWorkDay = entity.UsersInWorkDay.Select(u => _userInWorkDayUOWMapper.Map(u)).ToList()!
        };
        
        return res;
    }


    public static WorkDay? MapSimple(Domain.WorkDay? entity)
    {
        if (entity == null) return null;

        return new WorkDay()
        {
            Id = entity.Id,
            Day = entity.Day
        };
    }
    
    public static Domain.WorkDay? MapSimple(WorkDay? entity)
    {
        if (entity == null) return null;
        
        return new Domain.WorkDay()
        {
            Id = entity.Id,
            Day = entity.Day
        };
    }
}