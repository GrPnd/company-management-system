using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserInWorkDayRepository : BaseRepository<UserInWorkDay>, IUserInWorkDayRepository
{
    public UserInWorkDayRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}