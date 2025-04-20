using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class AbsenceMapper : IMapper<App.DAL.DTO.Absence, App.Domain.Absence>
{
    private readonly PersonMapper _personMapper = new();
    
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
            ByUser = _personMapper.Map(entity.ByUser)!,
            AuthorizedByUserId = entity.AuthorizedByUserId,
            AuthorizedByUser = _personMapper.Map(entity.AuthorizedByUser)!
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
            ByUser = _personMapper.Map(entity.ByUser)!,
            AuthorizedByUserId = entity.AuthorizedByUserId,
            AuthorizedByUser = _personMapper.Map(entity.AuthorizedByUser)!
        };
        
        return res;
    }
}