using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class UserInRoleService : BaseService<App.BLL.DTO.UserInRole, App.DAL.DTO.UserInRole>, IUserInRoleService
{
    public UserInRoleService(IBaseUOW serviceUOW, 
        IBaseRepository<UserInRole> serviceRepository, 
        IBLLMapper<DTO.UserInRole, UserInRole> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}