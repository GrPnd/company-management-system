using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class AbsenceBLLMapper : IBLLMapper<App.BLL.DTO.Absence, App.DAL.DTO.Absence>
{
    private readonly PersonBLLMapper _personUOWMapper = new();
    public Absence? Map(DTO.Absence? entity)
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
            ByUser = null,
            AuthorizedByUserId = entity.AuthorizedByUserId,
            AuthorizedByUser = null
        };
        
        return res;
    }

    public DTO.Absence? Map(Absence? entity)
    {
        if (entity == null) return null;
        
        var res = new DTO.Absence()
        {
            Id = entity.Id,
            Reason = entity.Reason,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsApproved = entity.IsApproved,
            ByUserId = entity.ByUserId,
            ByUser = null,
            AuthorizedByUserId = entity.AuthorizedByUserId,
            AuthorizedByUser = null
        };
        
        return res;
    }
}