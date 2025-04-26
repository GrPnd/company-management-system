using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class WorkDayService : BaseService<App.BLL.DTO.WorkDay, App.DAL.DTO.WorkDay, App.DAL.Contracts.Repositories.IWorkDayRepository>, IWorkDayService
{
    public WorkDayService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.WorkDay, WorkDay> bllMapper) : base(serviceUOW, serviceUOW.WorkDayRepository, bllMapper)
    {
    }
}