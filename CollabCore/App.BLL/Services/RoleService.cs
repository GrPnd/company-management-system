using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class RoleService : BaseService<App.BLL.DTO.Role, App.DAL.DTO.Role, App.DAL.Contracts.Repositories.IRoleRepository>, IRoleService
{
    public RoleService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Role, Role> bllMapper) : base(serviceUOW, serviceUOW.RoleRepository, bllMapper)
    {
    }
}