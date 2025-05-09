using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class TeamApiMapper : IApiMapper<ApiEntities.Team, App.BLL.DTO.Team>
{
    public Team? Map(BLL.DTO.Team? entity)
    {
        if (entity == null) return null;

        return new Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId
        };
    }

    public BLL.DTO.Team? Map(Team? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            DepartmentId = entity.DepartmentId
        };

    }
}