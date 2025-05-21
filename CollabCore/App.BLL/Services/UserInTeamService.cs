using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;

namespace App.BLL.Services;

public class UserInTeamService : BaseService<App.BLL.DTO.UserInTeam, App.DAL.DTO.UserInTeam, App.DAL.Contracts.Repositories.IUserInTeamRepository>, IUserInTeamService
{
    private readonly ITeamRoleService _teamRoleService;

    public UserInTeamService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.UserInTeam, UserInTeam> bllMapper, ITeamRoleService teamRoleService) : base(serviceUOW, serviceUOW.UserInTeamRepository, bllMapper)
    {
        _teamRoleService = teamRoleService;
    }
    
    public async Task<IEnumerable<App.BLL.DTO.UserInTeam?>> GetUserInTeamByPersonId(Guid personId)
    {
        var res = await ServiceRepository.GetUserInTeamByPersonId(personId);
        return res.Select(u => BLLMapper.Map(u)!);
    }

    public async Task<IEnumerable<App.BLL.DTO.UserInTeam?>> GetTeamLeadersInTeamByTeamId(Guid teamId)
    {
        var teamLeaderRoleId = (await _teamRoleService.AllAsync())
            .FirstOrDefault(r => r.Name == "Team Leader")?.Id;
        
        if (teamLeaderRoleId == null)
        {
            return [];
        }
        
        var allData = await ServiceRepository.AllAsync();

        return allData
            .Where(u => u.TeamId == teamId && u.TeamRoleId == teamLeaderRoleId)
            .Select(u => BLLMapper.Map(u)!);
    }
    
    public async Task<IEnumerable<App.BLL.DTO.UserInTeam?>> GetEnrichedUsersInTeam(Guid teamId)
    {
        var res = await ServiceRepository.GetEnrichedUsersInTeam(teamId);
        return res.Select(u => BLLMapper.Map(u));
    }

    public async Task<IEnumerable<DTO.UserInTeam?>> GetAllEnrichedUsersInTeam()
    {
        var res = await ServiceRepository.GetAllEnrichedUsersInTeam();
        return res.Select(u => BLLMapper.Map(u));
    }
}