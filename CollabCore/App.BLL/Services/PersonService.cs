using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;

namespace App.BLL.Services;

public class PersonService : BaseService<App.BLL.DTO.Person, App.DAL.DTO.Person, App.DAL.Contracts.Repositories.IPersonRepository>, IPersonService
{
    public PersonService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Person, Person> bllMapper) : base(serviceUOW, serviceUOW.PersonRepository, bllMapper)
    {
    }

    public async Task<IEnumerable<DTO.Person>> GetAdmins()
    {
        var res = await ServiceRepository.GetAdmins();
        return res.Select(u => BLLMapper.Map(u)!);
    }

    public async Task<DTO.Person?> FindByUserIdAsync(Guid userId)
    {
        var res = await ServiceRepository.FindByUserIdAsync(userId);
        return BLLMapper.Map(res);
    }
}