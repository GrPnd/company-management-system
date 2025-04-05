using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AbsenceRepository : BaseRepository<Absence>, IAbsenceRepository
{
    public AbsenceRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}