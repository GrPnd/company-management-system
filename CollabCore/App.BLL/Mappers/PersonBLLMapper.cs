using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class PersonBLLMapper : IBLLMapper<App.BLL.DTO.Person, App.DAL.DTO.Person>
{
    private readonly MessageBLLMapper _messageUOWMapper = new();
    private readonly TicketBLLMapper _ticketUOWMapper = new();
    private readonly AbsenceBLLMapper _absenceUOWMapper = new();
    private readonly WorkDayBLLMapper _workDayUOWMapper = new();
    private readonly UserInTeamBLLMapper _userInTeamUOWMapper = new();
    private readonly RoleBLLMapper _roleUOWMapper = new();
    public Person? Map(DTO.Person? entity)
    {
        if (entity == null) return null;

        return new Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => _messageUOWMapper.Map(m)).ToList(),
            ToMessages = entity.ToMessages?.Select(m => _messageUOWMapper.Map(m)).ToList(),
            FromTickets = entity.FromTickets?.Select(t => _ticketUOWMapper.Map(t)).ToList(),
            ByAbsences = entity.ByAbsences?.Select(a => _absenceUOWMapper.Map(a)).ToList(),
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => _absenceUOWMapper.Map(a)).ToList(),
            WorkDays = entity.WorkDays?.Select(wd => _workDayUOWMapper.Map(wd)).ToList(),
            UserInTeams = entity.UserInTeams?.Select(ut => _userInTeamUOWMapper.Map(ut)).ToList(),
            Roles = entity.Roles?.Select(r => _roleUOWMapper.Map(r)).ToList()
        };
    }

    public DTO.Person? Map(Person? entity)
    {
        if (entity == null) return null;

        return new DTO.Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => _messageUOWMapper.Map(m)).ToList(),
            ToMessages = entity.ToMessages?.Select(m => _messageUOWMapper.Map(m)).ToList(),
            FromTickets = entity.FromTickets?.Select(t => _ticketUOWMapper.Map(t)).ToList(),
            ByAbsences = entity.ByAbsences?.Select(a => _absenceUOWMapper.Map(a)).ToList(),
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => _absenceUOWMapper.Map(a)).ToList(),
            WorkDays = entity.WorkDays?.Select(wd => _workDayUOWMapper.Map(wd)).ToList(),
            UserInTeams = entity.UserInTeams?.Select(ut => _userInTeamUOWMapper.Map(ut)).ToList(),
            Roles = entity.Roles?.Select(r => _roleUOWMapper.Map(r)).ToList()
        };
    }
}