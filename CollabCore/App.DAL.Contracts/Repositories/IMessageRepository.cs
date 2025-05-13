using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts.Repositories;

public interface IMessageRepository: IBaseRepository<App.DAL.DTO.Message>, IMessageRepositoryCustom
{
}

public interface IMessageRepositoryCustom
{
    Task<IEnumerable<App.DAL.DTO.Message>> GetMessagesByPersonIdAsync(Guid personId);
}