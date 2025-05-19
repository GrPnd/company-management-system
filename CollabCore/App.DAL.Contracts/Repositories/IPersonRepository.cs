using Base.DAL.Contracts;

namespace App.DAL.Contracts.Repositories;

public interface IPersonRepository: IBaseRepository<App.DAL.DTO.Person>, IPersonRepositoryCustom
{
}

public interface IPersonRepositoryCustom
{
    Task<IEnumerable<App.DAL.DTO.Person>> GetAdmins();
    Task<App.DAL.DTO.Person?> FindByUserIdAsync(Guid userId);
}