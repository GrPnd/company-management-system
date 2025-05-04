using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class MessageUOWMapper : IUOWMapper<App.DAL.DTO.Message, App.Domain.Message>
{
    public Message? Map(Domain.Message? entity)
    {
        if (entity == null) return null;

        var res = new Message()
        {
            Id = entity.Id,
            Text = entity.Text,
            FromUserId = entity.FromUserId,
            FromUser = null,
            ToUserId = entity.ToUserId,
            ToUser = null,
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
            FromUser = null,
            ToUserId = entity.ToUserId,
            ToUser = null
        };
        
        return res;
    }
}