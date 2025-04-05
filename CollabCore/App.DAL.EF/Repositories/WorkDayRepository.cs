using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WorkDayRepository : BaseRepository<WorkDay>, IWorkDayRepository
{
    public WorkDayRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}