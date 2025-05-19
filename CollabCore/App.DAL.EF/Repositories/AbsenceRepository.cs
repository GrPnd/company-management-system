using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AbsenceRepository : BaseRepository<App.DAL.DTO.Absence, App.Domain.Absence>, IAbsenceRepository
{
    public AbsenceRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new AbsenceUOWMapper())
    {
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Absence?>> GetAbsencesSentToUserInTeam(Guid userId, Guid teamId)
    {
        var absences = await RepositoryDbSet
            .Include(a => a.ByUser)
            .ThenInclude(p => p!.UserInTeams)
            .Where(a => a.AuthorizedByUserId == userId &&
                        a.ByUser!.UserInTeams.Any(uit => uit.TeamId == teamId))
            .Select(a => UOWMapper.Map(a))
            .ToListAsync();

        return absences;
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Absence>> GetAbsencesSentByPersonId(Guid personId)
    {
        return await RepositoryDbSet
            .Where(a => a.ByUserId == personId)
            .Select(m => UOWMapper.Map(m)!)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Absence>> GetAbsencesSentToPersonId(Guid personId)
    {
        return await RepositoryDbSet
            .Where(a => a.AuthorizedByUserId == personId)
            .Select(m => UOWMapper.Map(m)!)
            .ToListAsync();
    }
}