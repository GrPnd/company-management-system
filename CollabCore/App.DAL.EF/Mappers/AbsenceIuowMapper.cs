using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class AbsenceIuowMapper : IUOWMapper<App.DAL.DTO.Absence, App.Domain.Absence>
{
    private readonly PersonIuowMapper _personIuowMapper = new();
    
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
            ByUser = _personIuowMapper.Map(entity.ByUser)!,
            AuthorizedByUserId = entity.AuthorizedByUserId,
            AuthorizedByUser = _personIuowMapper.Map(entity.AuthorizedByUser)!
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
            ByUser = _personIuowMapper.Map(entity.ByUser)!,
            AuthorizedByUserId = entity.AuthorizedByUserId,
            AuthorizedByUser = _personIuowMapper.Map(entity.AuthorizedByUser)!
        };
        
        return res;
    }
}