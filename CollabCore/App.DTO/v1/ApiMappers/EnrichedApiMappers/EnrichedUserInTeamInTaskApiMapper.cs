using App.DTO.v1.ApiEntities.EnrichedEntities;

namespace App.DTO.v1.ApiMappers.EnrichedApiMappers;

public class EnrichedUserInTeamInTaskApiMapper : IApiMapper<EnrichedUserInTeamInTask, App.BLL.DTO.Enriched.BLL.DTO.EnrichedUserInTeamInTask>
{
    public EnrichedUserInTeamInTask? Map(App.BLL.DTO.Enriched.BLL.DTO.EnrichedUserInTeamInTask? entity)
    {
        if (entity == null) return null;

        return new EnrichedUserInTeamInTask
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            Review = entity.Review,
            TaskId = entity.TaskId,
            UserInTeamId = entity.UserInTeamId,
            StatusId = entity.StatusId,
            StatusName = entity.StatusName,
            ParticipantNames = entity.ParticipantNames?.ToList()
        };
    }

    public App.BLL.DTO.Enriched.BLL.DTO.EnrichedUserInTeamInTask? Map(EnrichedUserInTeamInTask? entity)
    {
        if (entity == null) return null;

        return new App.BLL.DTO.Enriched.BLL.DTO.EnrichedUserInTeamInTask
        {
            Id = entity.Id,
            Since = entity.Since,
            Until = entity.Until,
            Review = entity.Review,
            TaskId = entity.TaskId,
            UserInTeamId = entity.UserInTeamId,
            StatusId = entity.StatusId,
            StatusName = entity.StatusName,
            ParticipantNames = entity.ParticipantNames?.ToList()
        };
    }
}