using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class ScheduleService : BaseService<App.BLL.DTO.Schedule, App.DAL.DTO.Schedule>, IScheduleService
{
    public ScheduleService(IBaseUOW serviceUOW, 
        IBaseRepository<Schedule> serviceRepository, 
        IBLLMapper<DTO.Schedule, Schedule> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}