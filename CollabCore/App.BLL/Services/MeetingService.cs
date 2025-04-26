using App.BLL.Contracts.Services;
using App.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;
using Meeting = App.DAL.DTO.Meeting;

namespace App.BLL.Services;

public class MeetingService : BaseService<App.BLL.DTO.Meeting, App.DAL.DTO.Meeting, App.DAL.Contracts.Repositories.IMeetingRepository>, IMeetingService
{
    public MeetingService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Meeting, Meeting> bllMapper) : base(serviceUOW, serviceUOW.MeetingRepository, bllMapper)
    {
    }
}