using App.DAL.DTO.Enriched.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers.EnrichedMappers;

public class EnrichedUserInTeamInTaskUOWMapper  : IUOWMapper<App.DAL.DTO.Enriched.DAL.DTO.EnrichedUserInTeamInTask, App.Domain.Enriched.EnrichedUserInTeamInTask>
{
    public EnrichedUserInTeamInTask? Map(App.Domain.Enriched.EnrichedUserInTeamInTask? entity)
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

    public App.Domain.Enriched.EnrichedUserInTeamInTask? Map(EnrichedUserInTeamInTask? entity)
    {
        if (entity == null) return null;

        return new Domain.Enriched.EnrichedUserInTeamInTask
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