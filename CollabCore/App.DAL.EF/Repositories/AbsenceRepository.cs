using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AbsenceRepository : BaseRepository<App.DAL.DTO.Absence, App.Domain.Absence>, IAbsenceRepository
{
    public AbsenceRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new AbsenceMapper())
    {
    }
}