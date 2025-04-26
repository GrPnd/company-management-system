using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class UserInWorkDayService : BaseService<App.BLL.DTO.UserInWorkDay, App.DAL.DTO.UserInWorkDay>, IUserInWorkDayService
{
    public UserInWorkDayService(IBaseUOW serviceUOW, 
        IBaseRepository<UserInWorkDay> serviceRepository, 
        IBLLMapper<DTO.UserInWorkDay, UserInWorkDay> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}