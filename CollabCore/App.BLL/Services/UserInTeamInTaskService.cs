using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class UserInTeamInTaskService : BaseService<App.BLL.DTO.UserInTeamInTask, App.DAL.DTO.UserInTeamInTask>, IUserInTeamInTaskService
{
    public UserInTeamInTaskService(IBaseUOW serviceUOW, 
        IBaseRepository<UserInTeamInTask> serviceRepository, 
        IBLLMapper<DTO.UserInTeamInTask, UserInTeamInTask> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}