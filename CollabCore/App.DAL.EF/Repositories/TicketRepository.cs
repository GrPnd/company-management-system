using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
{
    public TicketRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}