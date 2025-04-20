using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class PersonMapper : IMapper<App.DAL.DTO.Person, App.Domain.Person>
{
    private readonly MessageMapper _messageMapper = new();
    private readonly TicketMapper _ticketMapper = new();
    private readonly AbsenceMapper _absenceMapper = new();
    private readonly WorkDayMapper _workDayMapper = new();
    private readonly UserInTeamMapper _userInTeamMapper = new();
    private readonly RoleMapper _roleMapper = new();

    public Person? Map(Domain.Person? entity)
    {
        if (entity == null) return null;

        return new Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => _messageMapper.Map(m)).ToList(),
            ToMessages = entity.ToMessages?.Select(m => _messageMapper.Map(m)).ToList(),
            FromTickets = entity.FromTickets?.Select(t => _ticketMapper.Map(t)).ToList(),
            ByAbsences = entity.ByAbsences?.Select(a => _absenceMapper.Map(a)).ToList(),
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => _absenceMapper.Map(a)).ToList(),
            WorkDays = entity.WorkDays?.Select(wd => _workDayMapper.Map(wd)).ToList(),
            UserInTeams = entity.UserInTeams?.Select(ut => _userInTeamMapper.Map(ut)).ToList(),
            Roles = entity.Roles?.Select(r => _roleMapper.Map(r)).ToList()
        };
    }

    public Domain.Person? Map(Person? entity)
    {
        if (entity == null) return null;

        return new Domain.Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => _messageMapper.Map(m)).ToList(),
            ToMessages = entity.ToMessages?.Select(m => _messageMapper.Map(m)).ToList(),
            FromTickets = entity.FromTickets?.Select(t => _ticketMapper.Map(t)).ToList(),
            ByAbsences = entity.ByAbsences?.Select(a => _absenceMapper.Map(a)).ToList(),
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => _absenceMapper.Map(a)).ToList(),
            WorkDays = entity.WorkDays?.Select(wd => _workDayMapper.Map(wd)).ToList(),
            UserInTeams = entity.UserInTeams?.Select(ut => _userInTeamMapper.Map(ut)).ToList(),
            Roles = entity.Roles?.Select(r => _roleMapper.Map(r)).ToList()
        };
    }
}