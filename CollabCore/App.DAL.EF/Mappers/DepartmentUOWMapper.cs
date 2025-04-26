using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class DepartmentUOWMapper : IUOWMapper<App.DAL.DTO.Department, App.Domain.Department>
{
    public Department? Map(Domain.Department? entity)
    {
        if (entity == null) return null;

        var res = new Department()
        {
            Id = entity.Id,
            Name = entity.Name,
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
        };
        
        return res;
    }
}