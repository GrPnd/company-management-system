using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class RoleService : BaseService<App.BLL.DTO.Role, App.DAL.DTO.Role>, IRoleService
{
    public RoleService(
        IBaseUOW serviceUOW, 
        IBaseRepository<Role> serviceRepository, 
        IBLLMapper<DTO.Role, Role> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}