using Base.DAL.Contracts;

namespace App.DAL.Contracts.Repositories;

public interface IAbsenceRepository: IBaseRepository<App.DAL.DTO.Absence>, IAbsenceRepositoryCustom
{
}

public interface IAbsenceRepositoryCustom
{
    Task<IEnumerable<App.DAL.DTO.Absence?>> GetAbsencesSentToUserInTeam(Guid userId, Guid teamId);
    Task<IEnumerable<App.DAL.DTO.Absence>> GetAbsencesSentByPersonId(Guid personId);
    Task<IEnumerable<App.DAL.DTO.Absence>> GetAbsencesSentToPersonId(Guid personId);
}