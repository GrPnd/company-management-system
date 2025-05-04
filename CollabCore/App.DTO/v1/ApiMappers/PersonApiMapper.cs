using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class PersonApiMapper : IApiMapper<ApiEntities.Person, App.BLL.DTO.Person>
{
    public Person? Map(BLL.DTO.Person? entity)
    {
        if (entity == null) return null;

        return new Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName
        };
    }

    public BLL.DTO.Person? Map(Person? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName
        };
    }
}