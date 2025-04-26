using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class DepartmentApiMapper : IApiMapper<ApiEntities.Department, App.BLL.DTO.Department>
{
    public Department? Map(BLL.DTO.Department? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.Department? Map(Department? entity)
    {
        throw new NotImplementedException();
    }
}