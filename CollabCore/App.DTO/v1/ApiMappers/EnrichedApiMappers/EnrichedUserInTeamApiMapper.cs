using App.DTO.v1.ApiEntities.EnrichedEntities;

namespace App.DTO.v1.ApiMappers.EnrichedApiMappers;

public class EnrichedUserInTeamApiMapper : IApiMapper<EnrichedUserInTeam, App.BLL.DTO.UserInTeam>
{
    public EnrichedUserInTeam? Map(App.BLL.DTO.UserInTeam? entity)
    {
        if (entity == null) return null;

        return new EnrichedUserInTeam
        {
            Id = entity.Id,
            UserId = entity.UserId,
            PersonName = entity.User?.PersonName ?? "Unknown",
            TeamId = entity.TeamId,
            TeamName = entity.Team?.Name ?? "Unknown",
            TeamRoleId = entity.TeamRoleId,
            RoleName = entity.TeamRole?.Name ?? "Unknown",
            Since = entity.Since,
            Until = entity.Until
        };
    }

    public App.BLL.DTO.UserInTeam? Map(EnrichedUserInTeam? entity)
    {
        throw new NotImplementedException("Mapping from API to BLL is not required.");
    }
}
