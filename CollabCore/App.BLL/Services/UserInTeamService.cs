using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class UserInTeamService : BaseService<App.BLL.DTO.UserInTeam, App.DAL.DTO.UserInTeam>, IUserInTeamService
{
    public UserInTeamService(IBaseUOW serviceUOW, 
        IBaseRepository<UserInTeam> serviceRepository, 
        IBLLMapper<DTO.UserInTeam, UserInTeam> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}