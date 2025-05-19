using App.BLL.Contracts.Services;
using App.BLL.DTO.Enriched.BLL.DTO;
using App.BLL.Mappers.EnrichedMappers;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;


namespace App.BLL.Services;

public class UserInTeamInTaskService : BaseService<App.BLL.DTO.UserInTeamInTask, App.DAL.DTO.UserInTeamInTask, App.DAL.Contracts.Repositories.IUserInTeamInTaskRepository>, IUserInTeamInTaskService
{
    public UserInTeamInTaskService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.UserInTeamInTask, UserInTeamInTask> bllMapper) : base(serviceUOW, serviceUOW.UserInTeamInTaskRepository, bllMapper)
    {
    }

    public async Task<IEnumerable<EnrichedUserInTeamInTask>> GetEnrichedTasksForUserInTeam(Guid? userId, Guid teamId)
    {
        var res = await ServiceRepository.GetEnrichedTasksForUserInTeam(userId, teamId);
        var mapper = new EnrichedUserInTeamInTaskBLLMapper();
        return res.Select(e => mapper.Map(e)!);
    }
}