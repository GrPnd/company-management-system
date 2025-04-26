using Task = App.DTO.v1.ApiEntities.Task;

namespace App.DTO.v1.ApiMappers;

public class TaskApiMapper : IApiMapper<ApiEntities.Task, App.BLL.DTO.Task>
{
    public Task? Map(BLL.DTO.Task? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.Task? Map(Task? entity)
    {
        throw new NotImplementedException();
    }
}