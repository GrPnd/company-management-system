using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

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
}