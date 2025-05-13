using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class MessageService : BaseService<App.BLL.DTO.Message, App.DAL.DTO.Message, App.DAL.Contracts.Repositories.IMessageRepository>, IMessageService
{
    public MessageService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Message, Message> bllMapper) : base(serviceUOW, serviceUOW.MessageRepository, bllMapper)
    {
    }
    
    public async Task<IEnumerable<App.BLL.DTO.Message>> GetMessagesByPersonIdAsyncBLL(Guid personId)
    {
        var res = await GetMessagesByPersonIdAsync(personId);
        return res.Select(m => BLLMapper.Map(m)!);
    }

    public async Task<IEnumerable<App.DAL.DTO.Message>> GetMessagesByPersonIdAsync(Guid personId)
    {
        var result = await ServiceRepository.GetMessagesByPersonIdAsync(personId);
        return result;
    }
}