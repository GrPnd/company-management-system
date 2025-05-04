using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class PersonUOWMapper : IUOWMapper<App.DAL.DTO.Person, App.Domain.Person>
{
    public Person? Map(Domain.Person? entity)
    {
        if (entity == null) return null;

        return new Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => new Message()
            {
                Id = m.Id,
                Text = m.Text,
                FromUserId = m.FromUserId,
                FromUser = null,
                ToUserId = m.ToUserId,
                ToUser = null
            }).ToList(),
            ToMessages = entity.ToMessages?.Select(m => new Message()
            {
                Id = m.Id,
                Text = m.Text,
                FromUserId = m.FromUserId,
                FromUser = null,
                ToUserId = m.ToUserId,
                ToUser = null
            }).ToList(),
            FromTickets = entity.FromTickets?.Select(t => new Ticket()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                FromUserId = t.FromUserId,
                FromUser = null,
                ToUserId = t.ToUserId,
                ToUser = null
            }).ToList(),
            ByAbsences = entity.ByAbsences?.Select(a => new Absence()
            {
                Id = a.Id,
                Reason = a.Reason,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                IsApproved = a.IsApproved,
                ByUserId = a.ByUserId,
                ByUser = null,
                AuthorizedByUserId = a.AuthorizedByUserId,
                AuthorizedByUser = null
            }).ToList(),
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => new Absence()
            {
                Id = a.Id,
                Reason = a.Reason,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                IsApproved = a.IsApproved,
                ByUserId = a.ByUserId,
                ByUser = null,
                AuthorizedByUserId = a.AuthorizedByUserId,
                AuthorizedByUser = null
            }).ToList(),
            WorkDays = entity.WorkDays?.Select(w => new WorkDay()
            {
                Id = w.Id,
                Day = w.Day,
                UsersInWorkDay = w.UsersInWorkDay?.Select(u => new UserInWorkDay()
                {
                    Id = u.Id,
                    Since = u.Since,
                    Until = u.Until,
                    UserId = u.UserId,
                    User = null,
                    WorkDayId = u.WorkDayId,
                    WorkDay = null
                }).ToList()!
            }).ToList(),
            UserInTeams = entity.UserInTeams?.Select(u => new UserInTeam()
            {
                Id = u.Id,
                Role = u.Role,
                Since = u.Since,
                Until = u.Until,
                UserId = u.UserId,
                User = null,
                TeamId = u.TeamId,
                Team = null,
                Tasks = null,
                UserInTeamInTasks = null
            }).ToList(),
            Roles = entity.Roles?.Select(r => new Role()
            {
                Id = r.Id,
                Name = r.Name,
                Users = null
            }).ToList()
        };
    }

    public Domain.Person? Map(Person? entity)
    {
        if (entity == null) return null;

        return new Domain.Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => new Domain.Message()
            {
                Id = m.Id,
                Text = m.Text,
                FromUserId = m.FromUserId,
                FromUser = null,
                ToUserId = m.ToUserId,
                ToUser = null
            }).ToList(),
            ToMessages = entity.ToMessages?.Select(m => new Domain.Message()
            {
                Id = m.Id,
                Text = m.Text,
                FromUserId = m.FromUserId,
                FromUser = null,
                ToUserId = m.ToUserId,
                ToUser = null
            }).ToList(),
            FromTickets = entity.FromTickets?.Select(t => new Domain.Ticket()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                FromUserId = t.FromUserId,
                FromUser = null,
                ToUserId = t.ToUserId,
                ToUser = null
            }).ToList(),
            ByAbsences = entity.ByAbsences?.Select(a => new Domain.Absence()
            {
                Id = a.Id,
                Reason = a.Reason,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                IsApproved = a.IsApproved,
                ByUserId = a.ByUserId,
                ByUser = null,
                AuthorizedByUserId = a.AuthorizedByUserId,
                AuthorizedByUser = null
            }).ToList(),
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => new Domain.Absence()
            {
                Id = a.Id,
                Reason = a.Reason,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                IsApproved = a.IsApproved,
                ByUserId = a.ByUserId,
                ByUser = null,
                AuthorizedByUserId = a.AuthorizedByUserId,
                AuthorizedByUser = null
            }).ToList(),
            WorkDays = entity.WorkDays?.Select(w => new Domain.WorkDay()
            {
                Id = w.Id,
                Day = w.Day,
                UsersInWorkDay = w.UsersInWorkDay?.Select(u => new Domain.UserInWorkDay()
                {
                    Id = u.Id,
                    Since = u.Since,
                    Until = u.Until,
                    UserId = u.UserId,
                    User = null,
                    WorkDayId = u.WorkDayId,
                    WorkDay = null
                }).ToList()!
            }).ToList(),
            UserInTeams = entity.UserInTeams?.Select(u => new Domain.UserInTeam()
            {
                Id = u.Id,
                Role = u.Role,
                Since = u.Since,
                Until = u.Until,
                UserId = u.UserId,
                User = null,
                TeamId = u.TeamId,
                Team = null,
                Tasks = null,
                UserInTeamInTasks = null
            }).ToList(),
            Roles = entity.Roles?.Select(r => new Domain.Role()
            {
                Id = r.Id,
                Name = r.Name,
                Users = null
            }).ToList()
        };
    }
}