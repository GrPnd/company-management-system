using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class TeamRepository : BaseRepository<App.DAL.DTO.Team, App.Domain.Team>, ITeamRepository
{
    public TeamRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TeamUOWMapper())
    {
    }
    
}
