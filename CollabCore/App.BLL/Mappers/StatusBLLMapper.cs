using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class StatusBLLMapper : IBLLMapper<App.BLL.DTO.Status, App.DAL.DTO.Status>
{
    private readonly TaskBLLMapper _taskBLLMapper = new();
    public Status? Map(DTO.Status? entity)
    {
        if (entity == null) return null;

        var res = new Status()
        {
            Id = entity.Id,
            Name = entity.Name,
            Tasks = entity.Tasks?.Select(t => _taskBLLMapper.Map(t)).ToList()!
        };
        
        return res;
    }

    public DTO.Status? Map(Status? entity)
    {
        if (entity == null) return null;

        var res = new DTO.Status()
        {
            Id = entity.Id,
            Name = entity.Name,
            Tasks = entity.Tasks?.Select(t => _taskBLLMapper.Map(t)).ToList()!
        };
        
        return res;
    }
    
    public static Status? MapSimple(DTO.Status? entity)
    {
        if (entity == null) return null;

        return new Status()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    public static DTO.Status? MapSimple(Status? entity)
    {
        if (entity == null) return null;

        return new DTO.Status()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}