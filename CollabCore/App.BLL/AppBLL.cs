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
        _absenceService ??= new AbsenceService(BLLUOW, BLLUOW.AbsenceRepository, new AbsenceBLLMapper());
    

    private IDepartmentService? _departmentService;
    public IDepartmentService DepartmentService => 
        _departmentService ??= new DepartmentService(BLLUOW, BLLUOW.DepartmentRepository, new DepartmentBLLMapper());
    

    private IMeetingService? _meetingService;
    public IMeetingService MeetingService => 
        _meetingService ??= new MeetingService(BLLUOW, BLLUOW.MeetingRepository, new MeetingBLLMapper());
    

    private IMessageService? _messageService;
    public IMessageService MessageService => 
        _messageService ??= new MessageService(BLLUOW, BLLUOW.MessageRepository, new MessageBLLMapper());
    

    private IPersonService? _personService;
    public IPersonService PersonService => 
        _personService ??= new PersonService(BLLUOW, BLLUOW.PersonRepository, new PersonBLLMapper());
    

    private IRoleService? _roleService;
    public IRoleService RoleService => 
        _roleService ??= new RoleService(BLLUOW, BLLUOW.RoleRepository, new RoleBLLMapper());
    

    private IScheduleService? _scheduleService;
    public IScheduleService ScheduleService => 
        _scheduleService ??= new ScheduleService(BLLUOW, BLLUOW.ScheduleRepository, new ScheduleBLLMapper());
    

    private IStatusService? _statusService;
    public IStatusService StatusService =>
        _statusService ??= new StatusService(BLLUOW, BLLUOW.StatusRepository, new StatusBLLMapper());
    

    private ITaskService? _taskService;
    public ITaskService TaskService => 
        _taskService ??= new TaskService(BLLUOW, BLLUOW.TaskRepository, new TaskBLLMapper());
    
    private ITeamService? _teamService;
    public ITeamService TeamService => 
        _teamService ??= new TeamService(BLLUOW, BLLUOW.TeamRepository, new TeamBLLMapper());
    

    private ITicketService? _ticketService;
    public ITicketService TicketService => 
        _ticketService ??= new TicketService(BLLUOW, BLLUOW.TicketRepository, new TicketBLLMapper());
    

    private IUserInRoleService? _userInRoleService;
    public IUserInRoleService UserInRoleService => 
        _userInRoleService ??= new UserInRoleService(BLLUOW, BLLUOW.UserInRoleRepository, new UserInRoleBLLMapper());
    

    private IUserInTeamInTaskService? _userInTeamInTaskService;
    public IUserInTeamInTaskService UserInTeamInTaskService => 
        _userInTeamInTaskService ??= new UserInTeamInTaskService(BLLUOW, BLLUOW.UserInTeamInTaskRepository, new UserInTeamInTaskBLLMapper());
    

    private IUserInTeamService? _userInTeamService;
    public IUserInTeamService UserInTeamService => 
        _userInTeamService ??= new UserInTeamService(BLLUOW, BLLUOW.UserInTeamRepository, new UserInTeamBLLMapper());
    

    private IUserInWorkDayService? _userInWorkDayService;
    public IUserInWorkDayService UserInWorkDayService => 
        _userInWorkDayService ??= new UserInWorkDayService(BLLUOW, BLLUOW.UserInWorkDayRepository, new UserInWorkDayBLLMapper());
    

    private IWorkDayService? _workDayService;
    public IWorkDayService WorkDayService => 
        _workDayService ??= new WorkDayService(BLLUOW, BLLUOW.WorkDayRepository, new WorkDayBLLMapper());
}
