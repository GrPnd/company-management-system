using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class UserInRoleService : BaseService<App.BLL.DTO.UserInRole, App.DAL.DTO.UserInRole, App.DAL.Contracts.Repositories.IUserInRoleRepository>, IUserInRoleService
{
    public UserInRoleService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.UserInRole, UserInRole> bllMapper) : base(serviceUOW, serviceUOW.UserInRoleRepository, bllMapper)
    {
    }
}