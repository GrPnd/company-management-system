using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class DepartmentBLLMapper : IBLLMapper<App.BLL.DTO.Department, App.DAL.DTO.Department>
{
    private readonly TeamBLLMapper _teamBLLMapper = new();
    public Department? Map(DTO.Department? entity)
    {
        if (entity == null) return null;

        var res = new Department()
        {
            Id = entity.Id,
            Name = entity.Name,
            Teams = entity.Teams?.Select(t => _teamBLLMapper.Map(t)).ToList()!
        };
        
        return res;
    }

    public DTO.Department? Map(Department? entity)
    {
        if (entity == null) return null;

        var res = new DTO.Department()
        {
            Id = entity.Id,
            Name = entity.Name,
            Teams = entity.Teams?.Select(t => _teamBLLMapper.Map(t)).ToList()!
        };
        
        return res;
    }
    
    public static Department? MapSimple(DTO.Department? entity)
    {
        if (entity == null) return null;

        return new Department()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    public static DTO.Department? MapSimple(Department? entity)
    {
        if (entity == null) return null;

        return new DTO.Department()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}