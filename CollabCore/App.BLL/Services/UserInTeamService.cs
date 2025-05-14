using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;

namespace App.BLL.Services;

public class UserInTeamService : BaseService<App.BLL.DTO.UserInTeam, App.DAL.DTO.UserInTeam, App.DAL.Contracts.Repositories.IUserInTeamRepository>, IUserInTeamService
{
    public UserInTeamService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.UserInTeam, UserInTeam> bllMapper) : base(serviceUOW, serviceUOW.UserInTeamRepository, bllMapper)
    {
    }
    
    public async Task<IEnumerable<App.BLL.DTO.UserInTeam?>> GetUserInTeamByPersonId(Guid personId)
    {
        var res = await ServiceRepository.GetUserInTeamByPersonId(personId);
        return res.Select(u => BLLMapper.Map(u)!);
    }
}