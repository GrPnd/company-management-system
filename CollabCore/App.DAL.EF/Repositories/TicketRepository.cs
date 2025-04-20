using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TicketRepository : BaseRepository<App.DAL.DTO.Ticket, App.Domain.Ticket>, ITicketRepository
{
    public TicketRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new TicketMapper())
    {
    }
}