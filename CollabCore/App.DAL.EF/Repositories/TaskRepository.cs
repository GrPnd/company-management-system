using App.DAL.Contracts.Repositories;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Task = App.Domain.Task;

namespace App.DAL.EF.Repositories;

public class TaskRepository : BaseRepository<Task>, ITaskRepository
{
    public TaskRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}