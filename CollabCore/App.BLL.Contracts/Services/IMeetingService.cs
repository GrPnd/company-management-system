using Base.BLL.Contracts;

namespace App.BLL.Contracts.Services;

public interface IMeetingService: IBaseService<App.BLL.DTO.Meeting>
{
    Task<IEnumerable<App.BLL.DTO.Meeting?>> GetTeamMeetings(Guid personId);
}