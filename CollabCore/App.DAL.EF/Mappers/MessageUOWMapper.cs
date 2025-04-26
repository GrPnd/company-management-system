using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class MessageUOWMapper : IUOWMapper<App.DAL.DTO.Message, App.Domain.Message>
{
    private readonly PersonUOWMapper _personUOWMapper = new();
    public Message? Map(Domain.Message? entity)
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

    public Domain.Message? Map(Message? entity)
    {
        if (entity == null) return null;

        var res = new Domain.Message()
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