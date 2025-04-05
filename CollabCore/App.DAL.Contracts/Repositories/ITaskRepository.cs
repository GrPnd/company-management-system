using Base.DAL.Contracts;
using Task = App.Domain.Task;

namespace App.DAL.Contracts.Repositories;

public interface ITaskRepository: IBaseRepository<Task>
{
    
}