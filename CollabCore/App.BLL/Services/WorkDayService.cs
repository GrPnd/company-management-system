using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class WorkDayService : BaseService<App.BLL.DTO.WorkDay, App.DAL.DTO.WorkDay>, IWorkDayService
{
    public WorkDayService(IBaseUOW serviceUOW, 
        IBaseRepository<WorkDay> serviceRepository, 
        IBLLMapper<DTO.WorkDay, WorkDay> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}