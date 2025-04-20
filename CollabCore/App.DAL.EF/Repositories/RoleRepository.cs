using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RoleRepository : BaseRepository<App.DAL.DTO.Role, App.Domain.Role>, IRoleRepository
{
    public RoleRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new RoleMapper())
    {
    }
}