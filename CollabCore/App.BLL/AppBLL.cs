using App.BLL.Contracts;
using App.BLL.Contracts.Services;
using App.DAL.EF;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<AppUOW>, IAppBLL
{
    public AppBLL(AppUOW blluow) : base(blluow)
    {
    }

    private IAbsenceService _absenceService;
    private IDepartmentService _departmentService;
    private IMeetingService _meetingService;
    private IMessageService _messageService;
    private IPersonService _personService;
    private IRoleService _roleService;
    private IScheduleService _scheduleService;
    private IStatusService _statusService;
    private ITaskService _taskService;
    private ITicketService _ticketService;
    private IUserInRoleService _userInRoleService;
    private IUserInTeamInTaskService _userInTeamInTaskService;
    private IUserInTeamService _userInTeamService;
    private IUserInWorkDayService _userInWorkDayService;
    private IWorkDayService _workDayService;
}