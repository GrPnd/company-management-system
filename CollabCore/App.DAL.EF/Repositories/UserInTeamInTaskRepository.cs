using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.DAL.EF.Mappers.EnrichedMappers;
using App.Domain;
using App.Domain.Enriched;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserInTeamInTaskRepository : BaseRepository<App.DAL.DTO.UserInTeamInTask, App.Domain.UserInTeamInTask>,
    IUserInTeamInTaskRepository
{
    public UserInTeamInTaskRepository(DbContext repositoryDbContext) : base(repositoryDbContext,
        new UserInTeamInTaskUOWMapper())
    {
    }

    public async Task<IEnumerable<App.DAL.DTO.Enriched.DAL.DTO.EnrichedUserInTeamInTask>>
        GetEnrichedTasksForUserInTeam(Guid? userId, Guid teamId)
    {

        // 1. If userId is specified, get UserInTeam for that user in the team
        UserInTeam? userInTeam = null;
        if (userId.HasValue)
        {
            userInTeam = await RepositoryDbContext.Set<UserInTeam>()
                .Include(uit => uit.User)
                .FirstOrDefaultAsync(uit => uit.UserId == userId && uit.TeamId == teamId);

            if (userInTeam == null)
                return Enumerable.Empty<App.DAL.DTO.Enriched.DAL.DTO.EnrichedUserInTeamInTask>();
        }

        
        // 2. Get all UserInTeamInTask links for tasks in the given team
        // Since UserInTeamInTask only links users, teams are via UserInTeam, so we filter by team on UserInTeam
        var allUserTaskLinks = await RepositoryDbSet
            .Include(ut => ut.Task)
            .ThenInclude(t => t!.Status)
            .Include(ut => ut.UserInTeam) // Include UserInTeam navigation property to filter by team
            .ThenInclude(uit => uit!.User)
            .Where(ut => ut.UserInTeam!.TeamId == teamId) // Filter by teamId here
            .ToListAsync();

        
        // 3. Filter task links depending on whether userId was provided:
        // - If userId specified: filter only tasks for that userInTeam
        // - If userId null: use all tasks for the team (no filtering)
        List<UserInTeamInTask> userTaskLinks;
        if (userInTeam != null)
        {
            userTaskLinks = allUserTaskLinks
                .Where(ut => ut.UserInTeamId == userInTeam.Id)
                .ToList();
        }
        else
        {
            userTaskLinks = allUserTaskLinks;
        }

        
        // 4. Get all related task IDs
        var taskIds = userTaskLinks.Select(ut => ut.TaskId).Distinct().ToList();

        
        // 5. Get all UserInTeamInTask entries for those tasks (participants)
        var taskParticipants = allUserTaskLinks
            .Where(ut => taskIds.Contains(ut.TaskId))
            .ToList();

        
        // 6. Get related UserInTeams and Users for participants
        var userInTeamIds = taskParticipants.Select(tp => tp.UserInTeamId).Distinct().ToList();

        var relatedUserInTeams = await RepositoryDbContext.Set<UserInTeam>()
            .Include(uit => uit.User)
            .Where(uit => userInTeamIds.Contains(uit.Id))
            .ToListAsync();

        
        // 7. Build enriched tasks
        var enrichedTasks = new List<EnrichedUserInTeamInTask>();
        foreach (var link in userTaskLinks)
        {
            var task = link.Task;
            if (task == null) continue;

            var statusName = task.Status?.Name ?? "Unknown status name";

            // Participants excluding current user if userId specified; else include all participants except self not relevant (since no current user)
            var participants = taskParticipants
                .Where(tp => tp.TaskId == link.TaskId
                             && (userInTeam != null ? tp.UserInTeamId != userInTeam.Id : true))
                .Select(tp =>
                {
                    var participant = relatedUserInTeams.FirstOrDefault(u => u.Id == tp.UserInTeamId);
                    return participant?.User?.PersonName;
                })
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToList()!;

            enrichedTasks.Add(new EnrichedUserInTeamInTask
            {
                Id = link.Id,
                Since = link.Since,
                Until = link.Until,
                Review = link.Review,
                TaskId = link.TaskId,
                UserInTeamId = link.UserInTeamId,
                StatusId = task.StatusId,
                StatusName = statusName,
                ParticipantNames = participants
            });
        }

        var mapper = new EnrichedUserInTeamInTaskUOWMapper();
        return enrichedTasks.Select(e => mapper.Map(e)!);
    }
}