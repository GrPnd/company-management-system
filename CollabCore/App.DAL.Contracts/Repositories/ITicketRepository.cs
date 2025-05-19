using Base.DAL.Contracts;

namespace App.DAL.Contracts.Repositories;

public interface ITicketRepository: IBaseRepository<App.DAL.DTO.Ticket>, ITicketRepositoryCustom
{
}

public interface ITicketRepositoryCustom
{
    Task<IEnumerable<App.DAL.DTO.Ticket?>> GetTicketsSentToUserInTeam(Guid userId, Guid teamId);
    Task<IEnumerable<App.DAL.DTO.Ticket>> GetTicketsSentByPersonId(Guid personId);
    Task<IEnumerable<App.DAL.DTO.Ticket>> GetTicketsSentToPersonId(Guid personId);
}