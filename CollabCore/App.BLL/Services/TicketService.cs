using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class TicketService : BaseService<App.BLL.DTO.Ticket, App.DAL.DTO.Ticket>, ITicketService
{
    public TicketService(IBaseUOW serviceUOW, 
        IBaseRepository<Ticket> serviceRepository, 
        IBLLMapper<DTO.Ticket, Ticket> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}