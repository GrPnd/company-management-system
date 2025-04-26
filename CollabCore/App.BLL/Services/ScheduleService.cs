using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class ScheduleService : BaseService<App.BLL.DTO.Schedule, App.DAL.DTO.Schedule, App.DAL.Contracts.Repositories.IScheduleRepository>, IScheduleService
{
    public ScheduleService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Schedule, Schedule> bllMapper) : base(serviceUOW, serviceUOW.ScheduleRepository, bllMapper)
    {
    }
}