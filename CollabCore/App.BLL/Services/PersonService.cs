using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class PersonService : BaseService<App.BLL.DTO.Person, App.DAL.DTO.Person, App.DAL.Contracts.Repositories.IPersonRepository>, IPersonService
{
    public PersonService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Person, Person> bllMapper) : base(serviceUOW, serviceUOW.PersonRepository, bllMapper)
    {
    }
}