using App.BLL.Contracts.Services;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IAppBLL : IBaseBLL
{
    IAbsenceService AbsenceService { get; }
    IDepartmentService DepartmentService { get; }
    IMeetingService MeetingService { get; }
    IMessageService MessageService { get; }
    IPersonService PersonService { get; }
    IRoleService RoleService { get; }
    IScheduleService ScheduleService { get; }
    IStatusService StatusService { get; }
    ITaskService TaskService { get; }
    ITicketService TicketService { get; }
    IUserInRoleService UserInRoleService { get; }
    IUserInTeamInTaskService UserInTeamInTaskService { get; }
    IUserInTeamService UserInTeamService { get; }
    IUserInWorkDayService UserInWorkDayService { get; }
    IWorkDayService WorkDayService { get; }
}