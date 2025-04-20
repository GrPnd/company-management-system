using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserInWorkDayRepository : BaseRepository<App.DAL.DTO.UserInWorkDay, App.Domain.UserInWorkDay>, IUserInWorkDayRepository
{
    public UserInWorkDayRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new UserInWorkDayMapper())
    {
    }
}