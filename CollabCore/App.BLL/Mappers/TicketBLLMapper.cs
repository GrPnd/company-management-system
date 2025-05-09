using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class TicketBLLMapper : IBLLMapper<App.BLL.DTO.Ticket, App.DAL.DTO.Ticket>
{
    public Ticket? Map(DTO.Ticket? entity)
    {
        if (entity == null) return null;

        var res = new Ticket()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            FromUserId = entity.FromUserId,
            FromUser = PersonBLLMapper.MapSimple(entity.FromUser),
            ToUserId = entity.ToUserId,
            ToUser = PersonBLLMapper.MapSimple(entity.ToUser)
        };
        
        return res;
    }

    public DTO.Ticket? Map(Ticket? entity)
    {
        if (entity == null) return null;

        var res = new DTO.Ticket()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            FromUserId = entity.FromUserId,
            FromUser = PersonBLLMapper.MapSimple(entity.FromUser),
            ToUserId = entity.ToUserId,
            ToUser = PersonBLLMapper.MapSimple(entity.ToUser)
        };
        
        return res;
    }
}