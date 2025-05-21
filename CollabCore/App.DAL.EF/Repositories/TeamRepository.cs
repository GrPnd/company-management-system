using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TeamRepository : BaseRepository<App.DAL.DTO.Team, App.Domain.Team>, ITeamRepository
{
    public TeamRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new TeamUOWMapper())
    {
    }
}