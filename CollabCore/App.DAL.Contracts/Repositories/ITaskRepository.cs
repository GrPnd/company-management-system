using Base.DAL.Contracts;

namespace App.DAL.Contracts.Repositories;

public interface ITaskRepository: IBaseRepository<App.DAL.DTO.Task>, ITaskRepositoryCustom
{
}

public interface ITaskRepositoryCustom
{
    Task DeleteTaskWithTeamTaskRelation(Guid taskId);
}