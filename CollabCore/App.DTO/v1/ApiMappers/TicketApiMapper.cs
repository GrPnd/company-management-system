using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class TicketApiMapper : IApiMapper<ApiEntities.Ticket, App.BLL.DTO.Ticket>
{
    public Ticket? Map(BLL.DTO.Ticket? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.Ticket? Map(Ticket? entity)
    {
        throw new NotImplementedException();
    }
}