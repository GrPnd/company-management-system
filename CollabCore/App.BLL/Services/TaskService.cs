using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;


namespace App.BLL.Services;

public class TaskService : BaseService<App.BLL.DTO.Task, App.DAL.DTO.Task, App.DAL.Contracts.Repositories.ITaskRepository>, ITaskService
{
    public TaskService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Task, App.DAL.DTO.Task> bllMapper) : base(serviceUOW, serviceUOW.TaskRepository, bllMapper)
    {
    }

    public async Task DeleteTaskWithTeamTaskRelation(Guid taskId)
    {
        await ServiceRepository.DeleteTaskWithTeamTaskRelation(taskId);
    }
}