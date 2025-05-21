using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class DepartmentRepository : BaseRepository<App.DAL.DTO.Department, App.Domain.Department>, IDepartmentRepository
{
    public DepartmentRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new DepartmentUOWMapper())
    {
    }
}