using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class PersonBLLMapper : IBLLMapper<App.BLL.DTO.Person, App.DAL.DTO.Person>
{
    private readonly MessageBLLMapper _messageBLLMapper = new();
    private readonly TicketBLLMapper _ticketBLLMapper = new();
    private readonly AbsenceBLLMapper _absenceBLLMapper = new();
    private readonly WorkDayBLLMapper _workDayBLLMapper = new();
    private readonly UserInTeamBLLMapper _userInTeamBLLMapper = new();
    private readonly TeamRoleBLLMapper _teamRoleBLLMapper = new();
    public Person? Map(DTO.Person? entity)
    {
        if (entity == null) return null;

        return new Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => _messageBLLMapper.Map(m)).ToList()!,
            ToMessages = entity.ToMessages?.Select(m => _messageBLLMapper.Map(m)).ToList()!,
            FromTickets = entity.FromTickets?.Select(t => _ticketBLLMapper.Map(t)).ToList()!,
            ToTickets = entity.ToTickets?.Select(t => _ticketBLLMapper.Map(t)).ToList()!,
            ByAbsences = entity.ByAbsences?.Select(a => _absenceBLLMapper.Map(a)).ToList()!,
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => _absenceBLLMapper.Map(a)).ToList()!,
            WorkDays = entity.WorkDays?.Select(w => _workDayBLLMapper.Map(w)).ToList()!,
            UserInTeams = entity.UserInTeams?.Select(u => _userInTeamBLLMapper.Map(u)).ToList()!,
            TeamRoles = entity.TeamRoles?.Select(r => _teamRoleBLLMapper.Map(r)).ToList()!
        };
    }

    public DTO.Person? Map(Person? entity)
    {
        if (entity == null) return null;

        return new DTO.Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => _messageBLLMapper.Map(m)).ToList()!,
            ToMessages = entity.ToMessages?.Select(m => _messageBLLMapper.Map(m)).ToList()!,
            FromTickets = entity.FromTickets?.Select(t => _ticketBLLMapper.Map(t)).ToList()!,
            ToTickets = entity.ToTickets?.Select(t => _ticketBLLMapper.Map(t)).ToList()!,
            ByAbsences = entity.ByAbsences?.Select(a => _absenceBLLMapper.Map(a)).ToList()!,
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => _absenceBLLMapper.Map(a)).ToList()!,
            WorkDays = entity.WorkDays?.Select(w => _workDayBLLMapper.Map(w)).ToList()!,
            UserInTeams = entity.UserInTeams?.Select(u => _userInTeamBLLMapper.Map(u)).ToList()!,
            TeamRoles = entity.TeamRoles?.Select(r => _teamRoleBLLMapper.Map(r)).ToList()!
        };
    }
    
    public static Person? MapSimple(DTO.Person? entity)
    {
        if (entity == null) return null;

        return new Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName
        };
    }

    
    public static DTO.Person? MapSimple(Person? entity)
    {
        if (entity == null) return null;
        
        return new DTO.Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName
        };
    }
}