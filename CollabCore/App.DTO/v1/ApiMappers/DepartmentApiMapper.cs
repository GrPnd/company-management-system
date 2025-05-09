using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class DepartmentApiMapper : IApiMapper<ApiEntities.Department, App.BLL.DTO.Department>
{
    public Department? Map(BLL.DTO.Department? entity)
    {
        if (entity == null) return null;

        return new Department()
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public BLL.DTO.Department? Map(Department? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Department()
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}