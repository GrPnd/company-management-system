using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserInTeamInTaskRepository : BaseRepository<App.DAL.DTO.UserInTeamInTask, App.Domain.UserInTeamInTask>, IUserInTeamInTaskRepository
{
    public UserInTeamInTaskRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new UserInTeamInTaskUOWMapper())
    {
    }
}