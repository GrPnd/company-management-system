﻿using Base.BLL.Contracts;

namespace App.BLL.Contracts.Services;

public interface IUserInTeamService: IBaseService<App.BLL.DTO.UserInTeam>
{ 
    Task<IEnumerable<App.BLL.DTO.UserInTeam?>> GetUserInTeamByPersonId(Guid personId);
    Task<IEnumerable<App.BLL.DTO.UserInTeam?>> GetTeamLeadersInTeamByTeamId(Guid teamId);
    Task<IEnumerable<App.BLL.DTO.UserInTeam?>> GetEnrichedUsersInTeam(Guid teamId);
    Task<IEnumerable<App.BLL.DTO.UserInTeam?>> GetAllEnrichedUsersInTeam();
}
