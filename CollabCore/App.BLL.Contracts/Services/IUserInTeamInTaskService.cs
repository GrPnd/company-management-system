using App.BLL.DTO.Enriched.BLL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Contracts.Services;

public interface IUserInTeamInTaskService: IBaseService<App.BLL.DTO.UserInTeamInTask>
{
    Task<IEnumerable<EnrichedUserInTeamInTask>> GetEnrichedTasksForUserInTeam(Guid? userId, Guid teamId);
}