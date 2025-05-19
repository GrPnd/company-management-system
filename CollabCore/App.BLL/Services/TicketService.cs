using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;

namespace App.BLL.Services;

public class TicketService : BaseService<App.BLL.DTO.Ticket, App.DAL.DTO.Ticket, App.DAL.Contracts.Repositories.ITicketRepository>, ITicketService
{
    public TicketService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Ticket, Ticket> bllMapper) : base(serviceUOW, serviceUOW.TicketRepository, bllMapper)
    {
    }

    public async Task<IEnumerable<App.BLL.DTO.Ticket?>> GetTicketsSentToUserInTeam(Guid userId, Guid teamId)
    {
        var res = await ServiceRepository.GetTicketsSentToUserInTeam(userId, teamId);
        return res.Select(u => BLLMapper.Map(u)!);
    }

    public async Task<IEnumerable<DTO.Ticket>> GetTicketsSentByPersonId(Guid personId)
    {
        var res = await ServiceRepository.GetTicketsSentByPersonId(personId);
        return res.Select(u => BLLMapper.Map(u)!);
    }

    public async Task<IEnumerable<DTO.Ticket>> GetTicketsSentToPersonId(Guid personId)
    {
        var res = await ServiceRepository.GetTicketsSentToPersonId(personId);
        return res.Select(u => BLLMapper.Map(u)!);
    }
}