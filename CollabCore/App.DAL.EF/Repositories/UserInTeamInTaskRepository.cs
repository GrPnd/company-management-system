using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserInTeamInTaskRepository : BaseRepository<UserInTeamInTask>, IUserInTeamInTaskRepository
{
    public UserInTeamInTaskRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}