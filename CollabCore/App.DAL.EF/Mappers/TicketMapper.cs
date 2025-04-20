using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class TicketMapper : IMapper<App.DAL.DTO.Ticket, App.Domain.Ticket>
{
    private readonly PersonMapper _personMapper = new();
    
    public Ticket? Map(Domain.Ticket? entity)
    {
        if (entity == null) return null;

        var res = new Ticket()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            FromUserId = entity.FromUserId,
            FromUser = _personMapper.Map(entity.FromUser)!,
            ToUserId = entity.ToUserId,
            ToUser = _personMapper.Map(entity.ToUser)!
        };
        
        return res;
    }

    public Domain.Ticket? Map(Ticket? entity)
    {
        if (entity == null) return null;

        var res = new Domain.Ticket()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            FromUserId = entity.FromUserId,
            FromUser = _personMapper.Map(entity.FromUser)!,
            ToUserId = entity.ToUserId,
            ToUser = _personMapper.Map(entity.ToUser)!
        };
        
        return res;
    }
}