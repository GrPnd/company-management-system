using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserInTeamRepository : BaseRepository<App.DAL.DTO.UserInTeam, App.Domain.UserInTeam>, IUserInTeamRepository
{
    public UserInTeamRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new UserInTeamUOWMapper())
    {
    }
    
}
