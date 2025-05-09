using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class DepartmentUOWMapper : IUOWMapper<App.DAL.DTO.Department, App.Domain.Department>
{
    private readonly TeamUOWMapper _teamUOWMapper = new();
    public Department? Map(Domain.Department? entity)
    {
        if (entity == null) return null;

        var res = new Department()
        {
            Id = entity.Id,
            Name = entity.Name,
            Teams = entity.Teams?.Select(t => _teamUOWMapper.Map(t)).ToList()!
        };
        
        return res;
    }

    public Domain.Department? Map(Department? entity)
    {
        if (entity == null) return null;

        var res = new Domain.Department()
        {
            Id = entity.Id,
            Name = entity.Name,
            Teams = entity.Teams?.Select(t => _teamUOWMapper.Map(t)).ToList()!
        };
        
        return res;
    }

    public static Department? MapSimple(Domain.Department? entity)
    {
        if (entity == null) return null;

        return new Department()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    public static Domain.Department? MapSimple(Department? entity)
    {
        if (entity == null) return null;

        return new Domain.Department()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}