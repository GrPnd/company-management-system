using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class MessageBLLMapper : IBLLMapper<App.BLL.DTO.Message, App.DAL.DTO.Message>
{
    private readonly PersonBLLMapper _personUOWMapper = new();
    public Message? Map(DTO.Message? entity)
    {
        if (entity == null) return null;

        var res = new Message()
        {
            Id = entity.Id,
            Text = entity.Text,
            FromUserId = entity.FromUserId,
            FromUser = _personUOWMapper.Map(entity.FromUser)!,
            ToUserId = entity.ToUserId,
            ToUser = _personUOWMapper.Map(entity.ToUser)!
        };
        
        return res;
    }

    public DTO.Message? Map(Message? entity)
    {
        if (entity == null) return null;

        var res = new DTO.Message()
        {
            Id = entity.Id,
            Text = entity.Text,
            FromUserId = entity.FromUserId,
            FromUser = _personUOWMapper.Map(entity.FromUser)!,
            ToUserId = entity.ToUserId,
            ToUser = _personUOWMapper.Map(entity.ToUser)!
        };
        
        return res;
    }
}