using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class WorkDayBLLMapper : IBLLMapper<App.BLL.DTO.WorkDay, App.DAL.DTO.WorkDay>
{
    private readonly UserInWorkDayBLLMapper _userInWorkDayBLLMapper = new();
    public WorkDay? Map(DTO.WorkDay? entity)
    {
        if (entity == null) return null;

        var res = new WorkDay()
        {
            Id = entity.Id,
            Day = entity.Day,
            UsersInWorkDay = entity.UsersInWorkDay?.Select(u => _userInWorkDayBLLMapper.Map(u)).ToList()!
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
            UsersInWorkDay = entity.UsersInWorkDay?.Select(u => _userInWorkDayBLLMapper.Map(u)).ToList()!
        };
        
        return res;
    }
    
    public static WorkDay? MapSimple(DTO.WorkDay? entity)
    {
        if (entity == null) return null;

        return new WorkDay()
        {
            Id = entity.Id,
            Day = entity.Day
        };
    }
    
    public static DTO.WorkDay? MapSimple(WorkDay? entity)
    {
        if (entity == null) return null;
        
        return new DTO.WorkDay()
        {
            Id = entity.Id,
            Day = entity.Day
        };
    }
}