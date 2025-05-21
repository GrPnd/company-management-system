using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class StatusUOWMapper : IUOWMapper<App.DAL.DTO.Status, App.Domain.Status>
{
    private readonly TaskUOWMapper _taskUOWMapper = new();
    
    public Status? Map(Domain.Status? entity)
    {
        if (entity == null) return null;

        var res = new Status()
        {
            Id = entity.Id,
            Name = entity.Name,
            Tasks = entity.Tasks?.Select(t => _taskUOWMapper.Map(t)).ToList()!
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
            Tasks = entity.Tasks?.Select(t => _taskUOWMapper.Map(t)).ToList()!
        };
        
        return res;
    }

    public static Status? MapSimple(Domain.Status? entity)
    {
        if (entity == null) return null;

        return new Status()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    public static Domain.Status? MapSimple(Status? entity)
    {
        if (entity == null) return null;

        return new Domain.Status()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}