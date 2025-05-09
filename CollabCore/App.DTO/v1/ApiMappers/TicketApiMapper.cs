using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class TicketApiMapper : IApiMapper<ApiEntities.Ticket, App.BLL.DTO.Ticket>
{
    public Ticket? Map(BLL.DTO.Ticket? entity)
    {
        if (entity == null) return null;

        return new Ticket()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            ToUserId = entity.ToUserId,
            FromUserId = entity.FromUserId
        };
    }

    public BLL.DTO.Ticket? Map(Ticket? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Ticket()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            ToUserId = entity.ToUserId,
            FromUserId = entity.FromUserId
        };
    }
}