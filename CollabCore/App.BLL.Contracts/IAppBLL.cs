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
    ITeamRoleService TeamRoleService { get; }
    IStatusService StatusService { get; }
    ITaskService TaskService { get; }
    ITeamService TeamService { get; }
    ITicketService TicketService { get; }
    IUserInTeamInTaskService UserInTeamInTaskService { get; }
    IUserInTeamService UserInTeamService { get; }
}