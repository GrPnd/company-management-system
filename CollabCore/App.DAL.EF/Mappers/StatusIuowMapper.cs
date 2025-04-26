using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class StatusIuowMapper : IUOWMapper<App.DAL.DTO.Status, App.Domain.Status>
{
    private readonly TaskIuowMapper _taskIuowMapper = new();
    
    public Status? Map(Domain.Status? entity)
    {
        if (entity == null) return null;

        var res = new Status()
        {
            Id = entity.Id,
            Name = entity.Name,
            Tasks = entity.Tasks?.Select(_taskIuowMapper.Map).ToList()!,
        };
        
        return res;
    }

    public Domain.Status? Map(Status? entity)
    {
        if (entity == null) return null;

        var res = new Domain.Status()
        {
            Id = entity.Id,
            Name = entity.Name,
            Tasks = entity.Tasks?.Select(_taskIuowMapper.Map).ToList()!,
        };
        
        return res;
    }
}