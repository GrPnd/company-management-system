using App.BLL.Contracts;
using App.BLL.Contracts.Services;
using App.BLL.Mappers;
using App.BLL.Services;
using App.DAL.Contracts;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    public AppBLL(IAppUOW blluow) : base(blluow)
    {
    }

    private IAbsenceService? _absenceService;
    public IAbsenceService AbsenceService => 
        _absenceService ??= new AbsenceService(BLLUOW, new AbsenceBLLMapper());
    

    private IDepartmentService? _departmentService;
    public IDepartmentService DepartmentService => 
        _departmentService ??= new DepartmentService(BLLUOW, new DepartmentBLLMapper());
    

    private IMeetingService? _meetingService;
    public IMeetingService MeetingService => 
        _meetingService ??= new MeetingService(BLLUOW, new MeetingBLLMapper());
    

    private IMessageService? _messageService;
    public IMessageService MessageService => 
        _messageService ??= new MessageService(BLLUOW, new MessageBLLMapper());
    

    private IPersonService? _personService;
    public IPersonService PersonService => 
        _personService ??= new PersonService(BLLUOW, new PersonBLLMapper());
    

    private ITeamRoleService? _teamRoleService;
    public ITeamRoleService TeamRoleService => 
        _teamRoleService ??= new TeamRoleService(BLLUOW, new TeamRoleBLLMapper());
    

    private IScheduleService? _scheduleService;
    public IScheduleService ScheduleService => 
        _scheduleService ??= new ScheduleService(BLLUOW, new ScheduleBLLMapper());
    

    private IStatusService? _statusService;
    public IStatusService StatusService =>
        _statusService ??= new StatusService(BLLUOW, new StatusBLLMapper());
    

    private ITaskService? _taskService;
    public ITaskService TaskService => 
        _taskService ??= new TaskService(BLLUOW, new TaskBLLMapper());
    
    private ITeamService? _teamService;
    public ITeamService TeamService => 
        _teamService ??= new TeamService(BLLUOW, new TeamBLLMapper());
    

    private ITicketService? _ticketService;
    public ITicketService TicketService => 
        _ticketService ??= new TicketService(BLLUOW, new TicketBLLMapper());
    

    private IUserInTeamInTaskService? _userInTeamInTaskService;
    public IUserInTeamInTaskService UserInTeamInTaskService => 
        _userInTeamInTaskService ??= new UserInTeamInTaskService(BLLUOW, new UserInTeamInTaskBLLMapper());
    

    private IUserInTeamService? _userInTeamService;
    public IUserInTeamService UserInTeamService => 
        _userInTeamService ??= new UserInTeamService(BLLUOW, new UserInTeamBLLMapper());
    

    private IUserInWorkDayService? _userInWorkDayService;
    public IUserInWorkDayService UserInWorkDayService => 
        _userInWorkDayService ??= new UserInWorkDayService(BLLUOW, new UserInWorkDayBLLMapper());
    

    private IWorkDayService? _workDayService;
    public IWorkDayService WorkDayService => 
        _workDayService ??= new WorkDayService(BLLUOW, new WorkDayBLLMapper());
}
