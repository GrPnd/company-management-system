using App.BLL.Contracts.Services;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;
using Task = App.DAL.DTO.Task;

namespace App.BLL.Services;

public class TaskService : BaseService<App.BLL.DTO.Task, App.DAL.DTO.Task>, ITaskService
{
    public TaskService(IBaseUOW serviceUOW, 
        IBaseRepository<Task> serviceRepository, 
        IBLLMapper<DTO.Task, Task> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}