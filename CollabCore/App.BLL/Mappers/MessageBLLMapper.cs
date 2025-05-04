using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class MessageBLLMapper : IBLLMapper<App.BLL.DTO.Message, App.DAL.DTO.Message>
{
    public Message? Map(DTO.Message? entity)
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

    public DTO.Message? Map(Message? entity)
    {
        if (entity == null) return null;

        var res = new DTO.Message()
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
}