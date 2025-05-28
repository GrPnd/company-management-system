using App.DAL.DTO;
using Base.Contracts;

namespace App.DAL.EF.Mappers;

public class PersonUOWMapper : IMapper<App.DAL.DTO.Person, App.Domain.Person>
{
    public Person? MapTo(Domain.Person? entity)
    {
        if (entity == null) return null;

        return new Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName
        };
    }

    public Domain.Person? MapFrom(Person? entity)
    {
        if (entity == null) return null;

        return new Domain.Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName
        };
    }
    
    public static Person? MapSimple(Domain.Person? entity)
    {
        if (entity == null) return null;

        return new Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
        };
    }

    public static Domain.Person? MapSimple(Person? entity)
    {
        if (entity == null) return null;

        return new Domain.Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
        };
    }
}