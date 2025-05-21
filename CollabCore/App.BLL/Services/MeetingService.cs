using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Meeting = App.DAL.DTO.Meeting;

namespace App.BLL.Services;

public class MeetingService : BaseService<App.BLL.DTO.Meeting, App.DAL.DTO.Meeting, App.DAL.Contracts.Repositories.IMeetingRepository>, IMeetingService
{
    public MeetingService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Meeting, Meeting> bllMapper) : base(serviceUOW, serviceUOW.MeetingRepository, bllMapper)
    {
    }

    public async Task<IEnumerable<DTO.Meeting?>> GetTeamMeetings(Guid teamId)
    {
        var res = await ServiceRepository.GetTeamMeetings(teamId);
        return res.Select(u => BLLMapper.Map(u)!);
    }
}