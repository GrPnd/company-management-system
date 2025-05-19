using App.DAL.DTO.Enriched.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Contracts.Repositories;

public interface IUserInTeamInTaskRepository: IBaseRepository<App.DAL.DTO.UserInTeamInTask>, IUserInTeamInTaskRepositoryCustom
{
    
}

public interface IUserInTeamInTaskRepositoryCustom
{
    Task<IEnumerable<EnrichedUserInTeamInTask>> GetEnrichedTasksForUserInTeam(Guid? userId, Guid teamId);
}