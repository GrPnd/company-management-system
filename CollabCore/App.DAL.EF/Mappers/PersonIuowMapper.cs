using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class PersonIuowMapper : IUOWMapper<App.DAL.DTO.Person, App.Domain.Person>
{
    private readonly MessageIuowMapper _messageIuowMapper = new();
    private readonly TicketIuowMapper _ticketIuowMapper = new();
    private readonly AbsenceIuowMapper _absenceIuowMapper = new();
    private readonly WorkDayIuowMapper _workDayIuowMapper = new();
    private readonly UserInTeamIuowMapper _userInTeamIuowMapper = new();
    private readonly RoleIuowMapper _roleIuowMapper = new();

    public Person? Map(Domain.Person? entity)
    {
        if (entity == null) return null;

        return new Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => _messageIuowMapper.Map(m)).ToList(),
            ToMessages = entity.ToMessages?.Select(m => _messageIuowMapper.Map(m)).ToList(),
            FromTickets = entity.FromTickets?.Select(t => _ticketIuowMapper.Map(t)).ToList(),
            ByAbsences = entity.ByAbsences?.Select(a => _absenceIuowMapper.Map(a)).ToList(),
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => _absenceIuowMapper.Map(a)).ToList(),
            WorkDays = entity.WorkDays?.Select(wd => _workDayIuowMapper.Map(wd)).ToList(),
            UserInTeams = entity.UserInTeams?.Select(ut => _userInTeamIuowMapper.Map(ut)).ToList(),
            Roles = entity.Roles?.Select(r => _roleIuowMapper.Map(r)).ToList()
        };
    }

    public Domain.Person? Map(Person? entity)
    {
        if (entity == null) return null;

        return new Domain.Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => _messageIuowMapper.Map(m)).ToList(),
            ToMessages = entity.ToMessages?.Select(m => _messageIuowMapper.Map(m)).ToList(),
            FromTickets = entity.FromTickets?.Select(t => _ticketIuowMapper.Map(t)).ToList(),
            ByAbsences = entity.ByAbsences?.Select(a => _absenceIuowMapper.Map(a)).ToList(),
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => _absenceIuowMapper.Map(a)).ToList(),
            WorkDays = entity.WorkDays?.Select(wd => _workDayIuowMapper.Map(wd)).ToList(),
            UserInTeams = entity.UserInTeams?.Select(ut => _userInTeamIuowMapper.Map(ut)).ToList(),
            Roles = entity.Roles?.Select(r => _roleIuowMapper.Map(r)).ToList()
        };
    }
}