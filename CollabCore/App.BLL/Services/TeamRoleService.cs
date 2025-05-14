using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;

namespace App.BLL.Services;

public class TeamRoleService : BaseService<App.BLL.DTO.TeamRole, App.DAL.DTO.TeamRole, App.DAL.Contracts.Repositories.ITeamRoleRepository>, ITeamRoleService
{
    public TeamRoleService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.TeamRole, TeamRole> bllMapper) : base(serviceUOW, serviceUOW.TeamRoleRepository, bllMapper)
    {
    }
}