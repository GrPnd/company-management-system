using Base.DAL.Contracts;

namespace App.DAL.Contracts.Repositories;

public interface IUserInTeamRepository: IBaseRepository<App.DAL.DTO.UserInTeam>, IUserInTeamRepositoryCustom
{
}

public interface IUserInTeamRepositoryCustom
{
    Task<IEnumerable<App.DAL.DTO.UserInTeam?>> GetUserInTeamByPersonId(Guid personId);
    Task<IEnumerable<App.DAL.DTO.UserInTeam?>> GetEnrichedUsersInTeam(Guid teamId);
    Task<IEnumerable<App.DAL.DTO.UserInTeam?>> GetAllEnrichedUsersInTeam();
}