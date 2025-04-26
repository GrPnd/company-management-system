using App.BLL.Contracts.Services;
using App.BLL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;
using Meeting = App.DAL.DTO.Meeting;

namespace App.BLL.Services;

public class MeetingService : BaseService<App.BLL.DTO.Meeting, App.DAL.DTO.Meeting>, IMeetingService
{
    public MeetingService(
        IBaseUOW serviceUOW, 
        IBaseRepository<Meeting> serviceRepository, 
        IBLLMapper<DTO.Meeting, Meeting> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}