using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using UserInTeam = App.DAL.DTO.UserInTeam;

namespace App.DAL.EF.Repositories;

public class UserInTeamRepository : BaseRepository<App.DAL.DTO.UserInTeam, App.Domain.UserInTeam>, IUserInTeamRepository
{
    public UserInTeamRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new UserInTeamUOWMapper())
    {
    }
    
    public async Task<IEnumerable<App.DAL.DTO.UserInTeam?>> GetUserInTeamByPersonId(Guid personId)
    {
        var userInTeam = await RepositoryDbSet
            .Where(u => u.UserId == personId)
            .Select(u => UOWMapper.Map(u))
            .ToListAsync();

        return userInTeam;
    }

    public async Task<IEnumerable<App.DAL.DTO.UserInTeam?>> GetAllEnrichedUsersInTeam()
    {
        var result = await RepositoryDbSet
            .Include(u => u.User)
            .Include(u => u.Team)
            .Include(u => u.TeamRole)
            .Select(u => UOWMapper.Map(u))
            .ToListAsync();

        return result;
    }
    
    public async Task<IEnumerable<App.DAL.DTO.UserInTeam?>> GetEnrichedUsersInTeam(Guid teamId)
    {
        var result = await RepositoryDbSet
            .Where(u => u.TeamId == teamId)
            .Include(u => u.User)
            .Include(u => u.Team)
            .Include(u => u.TeamRole)
            .Select(u => UOWMapper.Map(u))
            .ToListAsync();

        return result;
    }
}