using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts.Repositories;

public interface IMeetingRepository: IBaseRepository<App.DAL.DTO.Meeting>, IMeetingRepositoryCustom
{
}

public interface IMeetingRepositoryCustom
{
    Task<IEnumerable<App.DAL.DTO.Meeting?>> GetTeamMeetings(Guid teamId);
}