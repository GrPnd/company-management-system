using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class MeetingApiMapper : IApiMapper<ApiEntities.Meeting, App.BLL.DTO.Meeting>
{
    public Meeting? Map(BLL.DTO.Meeting? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.Meeting? Map(Meeting? entity)
    {
        throw new NotImplementedException();
    }
}