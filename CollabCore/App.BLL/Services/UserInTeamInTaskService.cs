using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class UserInTeamInTaskService : BaseService<App.BLL.DTO.UserInTeamInTask, App.DAL.DTO.UserInTeamInTask, App.DAL.Contracts.Repositories.IUserInTeamInTaskRepository>, IUserInTeamInTaskService
{
    public UserInTeamInTaskService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.UserInTeamInTask, UserInTeamInTask> bllMapper) : base(serviceUOW, serviceUOW.UserInTeamInTaskRepository, bllMapper)
    {
    }
}