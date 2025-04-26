using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class TicketService : BaseService<App.BLL.DTO.Ticket, App.DAL.DTO.Ticket, App.DAL.Contracts.Repositories.ITicketRepository>, ITicketService
{
    public TicketService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Ticket, Ticket> bllMapper) : base(serviceUOW, serviceUOW.TicketRepository, bllMapper)
    {
    }
}