using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WorkDayRepository : BaseRepository<App.DAL.DTO.WorkDay, App.Domain.WorkDay>, IWorkDayRepository
{
    public WorkDayRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new WorkDayIuowMapper())
    {
    }
}