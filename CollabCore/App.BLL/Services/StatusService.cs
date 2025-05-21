using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;

namespace App.BLL.Services;

public class StatusService : BaseService<App.BLL.DTO.Status, App.DAL.DTO.Status, App.DAL.Contracts.Repositories.IStatusRepository>, IStatusService
{
    public StatusService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Status, Status> bllMapper) : base(serviceUOW, serviceUOW.StatusRepository, bllMapper)
    {
    }
}