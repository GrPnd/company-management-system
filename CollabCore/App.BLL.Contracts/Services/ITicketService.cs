using Base.BLL.Contracts;

namespace App.BLL.Contracts.Services;

public interface ITicketService: IBaseService<App.BLL.DTO.Ticket>
{
    Task<IEnumerable<App.BLL.DTO.Ticket?>> GetTicketsSentToUserInTeam(Guid userId, Guid teamId);
    Task<IEnumerable<App.BLL.DTO.Ticket>> GetTicketsSentByPersonId(Guid personId);
    Task<IEnumerable<App.BLL.DTO.Ticket>> GetTicketsSentToPersonId(Guid personId);
}