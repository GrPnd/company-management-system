using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class AbsenceApiMapper : IApiMapper<ApiEntities.Absence, App.BLL.DTO.Absence>
{
    public Absence? Map(BLL.DTO.Absence? entity)
    {
        if (entity == null) return null;

        return new Absence()
        {
            Id = entity.Id,
            Reason = entity.Reason,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsApproved = entity.IsApproved,
            ByUserId = entity.ByUserId,
            AuthorizedByUserId = entity.AuthorizedByUserId,
        };
    }

    public BLL.DTO.Absence? Map(Absence? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Absence()
        {
            Id = entity.Id,
            Reason = entity.Reason,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsApproved = entity.IsApproved,
            ByUserId = entity.ByUserId,
            AuthorizedByUserId = entity.AuthorizedByUserId,
        };
    }
}