using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class AbsenceUOWMapper : IUOWMapper<App.DAL.DTO.Absence, App.Domain.Absence>
{
    public Absence? Map(Domain.Absence? entity)
    {
        if (entity == null) return null;

        var res = new Absence()
        {
            Id = entity.Id,
            Reason = entity.Reason,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsApproved = entity.IsApproved,
            ByUserId = entity.ByUserId,
            ByUser = PersonUOWMapper.MapSimple(entity.ByUser),
            AuthorizedByUserId = entity.AuthorizedByUserId,
            AuthorizedByUser = PersonUOWMapper.MapSimple(entity.AuthorizedByUser)
        };
        
        return res;
    }

    public Domain.Absence? Map(Absence? entity)
    {
        if (entity == null) return null;
        
        var res = new Domain.Absence()
        {
            Id = entity.Id,
            Reason = entity.Reason,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsApproved = entity.IsApproved,
            ByUserId = entity.ByUserId,
            ByUser = PersonUOWMapper.MapSimple(entity.ByUser),
            AuthorizedByUserId = entity.AuthorizedByUserId,
            AuthorizedByUser = PersonUOWMapper.MapSimple(entity.AuthorizedByUser)
        };
        
        return res;
    }
}