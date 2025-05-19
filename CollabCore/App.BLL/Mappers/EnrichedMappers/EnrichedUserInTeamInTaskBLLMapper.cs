using App.BLL.DTO.Enriched.BLL.DTO;
using App.DAL.DTO.Enriched.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers.EnrichedMappers;

public class EnrichedUserInTeamInTaskBLLMapper : IBLLMapper<BLL.DTO.Enriched.BLL.DTO.EnrichedUserInTeamInTask, DAL.DTO.Enriched.DAL.DTO.EnrichedUserInTeamInTask>
{
    public BLL.DTO.Enriched.BLL.DTO.EnrichedUserInTeamInTask? Map(DAL.DTO.Enriched.DAL.DTO.EnrichedUserInTeamInTask? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Enriched.BLL.DTO.EnrichedUserInTeamInTask
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

    public DAL.DTO.Enriched.DAL.DTO.EnrichedUserInTeamInTask? Map(BLL.DTO.Enriched.BLL.DTO.EnrichedUserInTeamInTask? entity)
    {
        if (entity == null) return null;

        return new DAL.DTO.Enriched.DAL.DTO.EnrichedUserInTeamInTask
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