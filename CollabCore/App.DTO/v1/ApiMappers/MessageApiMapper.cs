using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class MessageApiMapper : IApiMapper<ApiEntities.Message, App.BLL.DTO.Message>
{
    public Message? Map(BLL.DTO.Message? entity)
    {
        if (entity == null) return null;

        return new Message()
        {
            Id = entity.Id,
            Text = entity.Text,
            ToUserId = entity.ToUserId,
            FromUserId = entity.FromUserId
        };
    }

    public BLL.DTO.Message? Map(Message? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Message()
        {
            Id = entity.Id,
            Text = entity.Text,
            ToUserId = entity.ToUserId,
            FromUserId = entity.FromUserId
        };
    }
}