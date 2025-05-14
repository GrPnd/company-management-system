using Base.BLL.Contracts;

namespace App.BLL.Contracts.Services;

public interface IUserInTeamService: IBaseService<App.BLL.DTO.UserInTeam>
{ 
    Task<IEnumerable<App.BLL.DTO.UserInTeam?>> GetUserInTeamByPersonId(Guid personId);
}
