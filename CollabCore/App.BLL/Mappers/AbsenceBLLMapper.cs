using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class AbsenceBLLMapper : IBLLMapper<App.BLL.DTO.Absence, App.DAL.DTO.Absence>
{
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
            ByUser = PersonBLLMapper.MapSimple(entity.ByUser),
            AuthorizedByUserId = entity.AuthorizedByUserId,
            AuthorizedByUser = PersonBLLMapper.MapSimple(entity.AuthorizedByUser)
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
            ByUser = PersonBLLMapper.MapSimple(entity.ByUser),
            AuthorizedByUserId = entity.AuthorizedByUserId,
            AuthorizedByUser = PersonBLLMapper.MapSimple(entity.AuthorizedByUser)
        };
        
        return res;
    }
}