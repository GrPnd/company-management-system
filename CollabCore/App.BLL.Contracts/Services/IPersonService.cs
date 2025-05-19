using Base.BLL.Contracts;

namespace App.BLL.Contracts.Services;

public interface IPersonService: IBaseService<App.BLL.DTO.Person>
{
    Task<IEnumerable<App.BLL.DTO.Person>> GetAdmins();
    Task<App.BLL.DTO.Person?> FindByUserIdAsync(Guid userId);
}