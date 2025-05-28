using Base.Contracts;

namespace App.DTO.v1.ApiMappers;

public class TeamApiMapper : IMapper<ApiEntities.Team, App.DAL.DTO.Team>
{
    public ApiEntities.Team? MapTo(App.DAL.DTO.Team? entity)
    {
        if (entity == null) return null;

        var res = new ApiEntities.Team()
        {
            Id = entity.Id,
            TeamName = entity.TeamName,
        };
        return res;
    }

    public App.DAL.DTO.Team? MapFrom(ApiEntities.Team? entity)
    {
        if (entity == null) return null;
        var res = new App.DAL.DTO.Team()
        {
            Id = entity.Id,
            TeamName = entity.TeamName,
        };
        return res;
    }
    
}
