using Base.BLL.Contracts;

namespace App.BLL.Contracts.Services;

public interface IAbsenceService: IBaseService<App.BLL.DTO.Absence>
{
    Task<IEnumerable<App.BLL.DTO.Absence?>> GetAbsencesSentToUserInTeam(Guid userId, Guid teamId);
    Task<IEnumerable<App.BLL.DTO.Absence>> GetAbsencesSentByPersonId(Guid personId);
    Task<IEnumerable<App.BLL.DTO.Absence>> GetAbsencesSentToPersonId(Guid personId);
}