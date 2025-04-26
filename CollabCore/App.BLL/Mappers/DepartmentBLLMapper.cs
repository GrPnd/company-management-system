using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class DepartmentBLLMapper : IBLLMapper<App.BLL.DTO.Department, App.DAL.DTO.Department>
{
    public Department? Map(DTO.Department? entity)
    {
        if (entity == null) return null;

        var res = new Department()
        {
            Id = entity.Id,
            Name = entity.Name,
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
        };
        
        return res;
    }
}