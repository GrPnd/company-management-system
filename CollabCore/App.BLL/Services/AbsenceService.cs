using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class AbsenceService : BaseService<App.BLL.DTO.Absence, App.DAL.DTO.Absence>, IAbsenceService
{
    public AbsenceService(
        IBaseUOW serviceUOW, 
        IBaseRepository<Absence> serviceRepository, 
        IBLLMapper<DTO.Absence, Absence> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}