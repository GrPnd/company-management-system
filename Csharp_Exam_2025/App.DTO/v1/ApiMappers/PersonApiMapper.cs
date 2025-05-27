using App.DTO.v1.ApiEntities;
using Base.Contracts;

namespace App.DTO.v1.ApiMappers;

public class PersonApiMapper : IMapper<App.DTO.v1.ApiEntities.Person, App.DAL.DTO.Person>
{
    public Person? MapTo(DAL.DTO.Person? entity)
    {
        if (entity == null) return null;

        return new Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName
        };
    }

    public DAL.DTO.Person? MapFrom(Person? entity)
    {
        if (entity == null) return null;

        return new DAL.DTO.Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName
        };
    }
}