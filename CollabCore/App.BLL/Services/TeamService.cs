using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;

namespace App.BLL.Services;

public class TeamService : BaseService<App.BLL.DTO.Team, App.DAL.DTO.Team, App.DAL.Contracts.Repositories.ITeamRepository>, ITeamService
{
    public TeamService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Team, Team> bllMapper) : base(serviceUOW, serviceUOW.TeamRepository, bllMapper)
    {
    }
}