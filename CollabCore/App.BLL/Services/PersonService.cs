using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class PersonService : BaseService<App.BLL.DTO.Person, App.DAL.DTO.Person>, IPersonService
{
    public PersonService(
        IBaseUOW serviceUOW, 
        IBaseRepository<Person> serviceRepository, 
        IBLLMapper<DTO.Person, Person> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}