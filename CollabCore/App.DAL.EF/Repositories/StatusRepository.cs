using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class StatusRepository : BaseRepository<Status>, IStatusRepository
{
    public StatusRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}