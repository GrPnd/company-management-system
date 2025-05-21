using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MeetingRepository : BaseRepository<App.DAL.DTO.Meeting, App.Domain.Meeting>, IMeetingRepository
{
    public MeetingRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new MeetingUOWMapper())
    {
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Meeting?>> GetTeamMeetings(Guid teamId)
    {
        var userInTeam = await RepositoryDbSet
            .Where(u => u.TeamId == teamId)
            .Select(u => UOWMapper.Map(u))
            .ToListAsync();

        return userInTeam;
    }
}