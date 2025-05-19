using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TicketRepository : BaseRepository<App.DAL.DTO.Ticket, App.Domain.Ticket>, ITicketRepository
{
    public TicketRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new TicketUOWMapper())
    {
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Ticket?>> GetTicketsSentToUserInTeam(Guid userId, Guid teamId)
    {
        var tickets = await RepositoryDbSet
            .Include(a => a.FromUser)
            .ThenInclude(p => p!.UserInTeams)
            .Where(a => a.ToUserId == userId &&
                        a.FromUser!.UserInTeams.Any(uit => uit.TeamId == teamId))
            .Select(a => UOWMapper.Map(a))
            .ToListAsync();

        return tickets;
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Ticket>> GetTicketsSentByPersonId(Guid personId)
    {
        return await RepositoryDbSet
            .Where(a => a.FromUserId == personId)
            .Select(m => UOWMapper.Map(m)!)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Ticket>> GetTicketsSentToPersonId(Guid personId)
    {
        return await RepositoryDbSet
            .Where(a => a.ToUserId == personId)
            .Select(m => UOWMapper.Map(m)!)
            .ToListAsync();
    }
}