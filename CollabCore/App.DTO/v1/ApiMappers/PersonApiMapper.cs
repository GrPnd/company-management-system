using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class PersonApiMapper : IApiMapper<ApiEntities.Person, App.BLL.DTO.Person>
{
    public Person? Map(BLL.DTO.Person? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.Person? Map(Person? entity)
    {
        throw new NotImplementedException();
    }
}