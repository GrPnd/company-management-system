using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserInRoleRepository : BaseRepository<UserInRole>, IUserInRoleRepository
{
    public UserInRoleRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}