using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class TeamRoleApiMapper : IApiMapper<ApiEntities.TeamRole, App.BLL.DTO.TeamRole>
{
    public TeamRole? Map(BLL.DTO.TeamRole? entity)
    {
        if (entity == null) return null;

        return new TeamRole()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public BLL.DTO.TeamRole? Map(TeamRole? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.TeamRole()
        {
            Id = entity.Id,
            Name = entity.Name
        };

    }
}