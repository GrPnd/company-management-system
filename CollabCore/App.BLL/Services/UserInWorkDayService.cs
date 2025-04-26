using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class UserInWorkDayService : BaseService<App.BLL.DTO.UserInWorkDay, App.DAL.DTO.UserInWorkDay, App.DAL.Contracts.Repositories.IUserInWorkDayRepository>, IUserInWorkDayService
{
    public UserInWorkDayService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.UserInWorkDay, UserInWorkDay> bllMapper) : base(serviceUOW, serviceUOW.UserInWorkDayRepository, bllMapper)
    {
    }
}