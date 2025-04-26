using App.BLL.Contracts.Services;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class MessageService : BaseService<App.BLL.DTO.Message, App.DAL.DTO.Message>, IMessageService
{
    public MessageService(
        IBaseUOW serviceUOW, 
        IBaseRepository<Message> serviceRepository, 
        IBLLMapper<DTO.Message, Message> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}