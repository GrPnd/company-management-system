using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class MessageApiMapper : IApiMapper<ApiEntities.Message, App.BLL.DTO.Message>
{
    public Message? Map(BLL.DTO.Message? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.Message? Map(Message? entity)
    {
        throw new NotImplementedException();
    }
}