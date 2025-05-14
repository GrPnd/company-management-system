using App.DAL.Contracts.Repositories;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IAppUOW : IBaseUOW
{
    IAbsenceRepository AbsenceRepository { get; }
    IDepartmentRepository DepartmentRepository { get; }
    IMeetingRepository MeetingRepository { get; }
    IMessageRepository MessageRepository { get; }
    IPersonRepository PersonRepository { get; }
    ITeamRoleRepository TeamRoleRepository { get; }
    IScheduleRepository ScheduleRepository { get; }
    IStatusRepository StatusRepository { get; }
    ITaskRepository TaskRepository { get; }
    ITeamRepository TeamRepository { get; }
    ITicketRepository TicketRepository { get; }
    IUserInTeamInTaskRepository UserInTeamInTaskRepository { get; }
    IUserInTeamRepository UserInTeamRepository { get; }
    IUserInWorkDayRepository UserInWorkDayRepository { get; }
    IWorkDayRepository WorkDayRepository { get; }
}