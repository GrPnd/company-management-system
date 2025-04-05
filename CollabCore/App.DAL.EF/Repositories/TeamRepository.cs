using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TeamRepository : BaseRepository<Team>, ITeamRepository
{
    public TeamRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}