using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class AbsenceService : BaseService<App.BLL.DTO.Absence, App.DAL.DTO.Absence, App.DAL.Contracts.Repositories.IAbsenceRepository>, IAbsenceService
{
    public AbsenceService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Absence, Absence> bllMapper) : base(serviceUOW, serviceUOW.AbsenceRepository, bllMapper)
    {
    }
}