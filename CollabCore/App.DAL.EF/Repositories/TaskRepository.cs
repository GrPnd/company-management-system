using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Task = App.Domain.Task;

namespace App.DAL.EF.Repositories;

public class TaskRepository : BaseRepository<App.DAL.DTO.Task, App.Domain.Task>, ITaskRepository
{
    public TaskRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new TaskUOWMapper())
    {
    }
}