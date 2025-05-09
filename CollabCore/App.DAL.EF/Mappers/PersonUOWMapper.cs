using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class PersonUOWMapper : IUOWMapper<App.DAL.DTO.Person, App.Domain.Person>
{
    private readonly MessageUOWMapper _messageUOWMapper = new();
    private readonly TicketUOWMapper _ticketUOWMapper = new();
    private readonly AbsenceUOWMapper _absenceUOWMapper = new();
    private readonly WorkDayUOWMapper _workDayUOWMapper = new();
    private readonly UserInTeamUOWMapper _userInTeamUOWMapper = new();
    private readonly RoleUOWMapper _roleUOWMapper = new();
    
    public Person? Map(Domain.Person? entity)
    {
        if (entity == null) return null;

        return new Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => _messageUOWMapper.Map(m)).ToList()!,
            ToMessages = entity.ToMessages?.Select(m => _messageUOWMapper.Map(m)).ToList()!,
            FromTickets = entity.FromTickets?.Select(t => _ticketUOWMapper.Map(t)).ToList()!,
            ToTickets = entity.ToTickets?.Select(t => _ticketUOWMapper.Map(t)).ToList()!,
            ByAbsences = entity.ByAbsences?.Select(a => _absenceUOWMapper.Map(a)).ToList()!,
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => _absenceUOWMapper.Map(a)).ToList()!,
            WorkDays = entity.WorkDays?.Select(w => _workDayUOWMapper.Map(w)).ToList()!,
            UserInTeams = entity.UserInTeams?.Select(u => _userInTeamUOWMapper.Map(u)).ToList()!,
            Roles = entity.Roles?.Select(r => _roleUOWMapper.Map(r)).ToList()!
        };
    }

    public Domain.Person? Map(Person? entity)
    {
        if (entity == null) return null;

        return new Domain.Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => _messageUOWMapper.Map(m)).ToList()!,
            ToMessages = entity.ToMessages?.Select(m => _messageUOWMapper.Map(m)).ToList()!,
            FromTickets = entity.FromTickets?.Select(t => _ticketUOWMapper.Map(t)).ToList()!,
            ToTickets = entity.ToTickets?.Select(t => _ticketUOWMapper.Map(t)).ToList()!,
            ByAbsences = entity.ByAbsences?.Select(a => _absenceUOWMapper.Map(a)).ToList()!,
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => _absenceUOWMapper.Map(a)).ToList()!,
            WorkDays = entity.WorkDays?.Select(w => _workDayUOWMapper.Map(w)).ToList()!,
            UserInTeams = entity.UserInTeams?.Select(u => _userInTeamUOWMapper.Map(u)).ToList()!,
            Roles = entity.Roles?.Select(r => _roleUOWMapper.Map(r)).ToList()!
        };
    }


    public static Person? MapSimple(Domain.Person? entity)
    {
        if (entity == null) return null;

        return new Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName
        };
    }

    
    public static Domain.Person? MapSimple(Person? entity)
    {
        if (entity == null) return null;
        
        return new Domain.Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName
        };
    }
}