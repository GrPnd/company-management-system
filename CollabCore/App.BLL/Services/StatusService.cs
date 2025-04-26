using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class StatusService : BaseService<App.BLL.DTO.Status, App.DAL.DTO.Status>, IStatusService
{
    public StatusService(IBaseUOW serviceUOW, 
        IBaseRepository<Status> serviceRepository, 
        IBLLMapper<DTO.Status, Status> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}