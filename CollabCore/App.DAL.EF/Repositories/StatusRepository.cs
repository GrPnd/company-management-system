using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class StatusRepository : BaseRepository<App.DAL.DTO.Status, App.Domain.Status>, IStatusRepository
{
    public StatusRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new StatusIuowMapper())
    {
    }
}