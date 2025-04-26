using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserInRoleRepository : BaseRepository<App.DAL.DTO.UserInRole, App.Domain.UserInRole>, IUserInRoleRepository
{
    public UserInRoleRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new UserInRoleUOWMapper())
    {
    }
}