using App.DAL.Contracts.Repositories;
using Base.BLL.Contracts;

namespace App.BLL.Contracts.Services;

public interface IMessageService: IBaseService<App.BLL.DTO.Message>
{
    Task<IEnumerable<App.BLL.DTO.Message>> GetMessagesByPersonIdAsync(Guid personId);
}