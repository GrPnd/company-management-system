using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class TeamApiMapper : IApiMapper<ApiEntities.Team, App.BLL.DTO.Team>
{
    public Team? Map(BLL.DTO.Team? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.Team? Map(Team? entity)
    {
        throw new NotImplementedException();
    }
}