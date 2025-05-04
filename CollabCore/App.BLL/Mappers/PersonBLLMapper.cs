using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class PersonBLLMapper : IBLLMapper<App.BLL.DTO.Person, App.DAL.DTO.Person>
{
    public Person? Map(DTO.Person? entity)
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

    public DTO.Person? Map(Person? entity)
    {
        if (entity == null) return null;

        return new DTO.Person
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            FromMessages = entity.FromMessages?.Select(m => new DTO.Message()
            {
                Id = m.Id,
                Text = m.Text,
                FromUserId = m.FromUserId,
                FromUser = null,
                ToUserId = m.ToUserId,
                ToUser = null
            }).ToList(),
            ToMessages = entity.ToMessages?.Select(m => new DTO.Message()
            {
                Id = m.Id,
                Text = m.Text,
                FromUserId = m.FromUserId,
                FromUser = null,
                ToUserId = m.ToUserId,
                ToUser = null
            }).ToList(),
            FromTickets = entity.FromTickets?.Select(t => new DTO.Ticket()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                FromUserId = t.FromUserId,
                FromUser = null,
                ToUserId = t.ToUserId,
                ToUser = null
            }).ToList(),
            ByAbsences = entity.ByAbsences?.Select(a => new DTO.Absence()
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
            AuthorizedByAbsences = entity.AuthorizedByAbsences?.Select(a => new DTO.Absence()
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
            WorkDays = entity.WorkDays?.Select(w => new DTO.WorkDay()
            {
                Id = w.Id,
                Day = w.Day,
                UsersInWorkDay = w.UsersInWorkDay?.Select(u => new DTO.UserInWorkDay()
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
            UserInTeams = entity.UserInTeams?.Select(u => new DTO.UserInTeam()
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
            Roles = entity.Roles?.Select(r => new DTO.Role()
            {
                Id = r.Id,
                Name = r.Name,
                Users = null
            }).ToList()
        };
    }
}