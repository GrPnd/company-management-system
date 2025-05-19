using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace App.DAL.EF.Repositories;

public class TaskRepository : BaseRepository<App.DAL.DTO.Task, App.Domain.Task>, ITaskRepository
{
    public TaskRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new TaskUOWMapper())
    {
    }
    
    public async Task DeleteTaskWithTeamTaskRelation(Guid taskId)
    {
        await RepositoryDbContext.Set<UserInTeamInTask>()
            .Where(x => x.TaskId == taskId)
            .ExecuteDeleteAsync();

        await RemoveAsync(taskId);
    }
}