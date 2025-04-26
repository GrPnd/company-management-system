using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class TeamService : BaseService<App.BLL.DTO.Team, App.DAL.DTO.Team>, ITeamService
{
    public TeamService(IBaseUOW serviceUOW, 
        IBaseRepository<Team> serviceRepository, 
        IBLLMapper<DTO.Team, Team> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}