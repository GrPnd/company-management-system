using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TeamRoleRepository : BaseRepository<App.DAL.DTO.TeamRole, App.Domain.TeamRole>, ITeamRoleRepository
{
    public TeamRoleRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new TeamRoleUOWMapper())
    {
    }
}