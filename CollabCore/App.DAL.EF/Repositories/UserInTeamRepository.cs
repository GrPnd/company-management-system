using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserInTeamRepository : BaseRepository<UserInTeam>, IUserInTeamRepository
{
    public UserInTeamRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}