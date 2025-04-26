using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ScheduleRepository : BaseRepository<App.DAL.DTO.Schedule, App.Domain.Schedule>, IScheduleRepository
{
    public ScheduleRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new ScheduleIuowMapper())
    {
    }
}