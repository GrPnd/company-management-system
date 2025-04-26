using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUow<AppDbContext>, IAppUOW
{
    public AppUOW(AppDbContext uowDbContext) : base(uowDbContext)
    {
        
    }
    
    private IAbsenceRepository? _absenceRepository;
    public IAbsenceRepository AbsenceRepository => _absenceRepository ??= new AbsenceRepository(UowDbContext);
    
    
    private IDepartmentRepository? _departmentRepository;
    public IDepartmentRepository DepartmentRepository => _departmentRepository ??= new DepartmentRepository(UowDbContext);
    
    
    private IMeetingRepository? _meetingRepository;
    public IMeetingRepository MeetingRepository => _meetingRepository ??= new MeetingRepository(UowDbContext);
    
    
    private IMessageRepository? _messageRepository;
    public IMessageRepository MessageRepository => _messageRepository ??= new MessageRepository(UowDbContext);
    
    
    private IPersonRepository? _personRepository;
    public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(UowDbContext);
    
    
    private IRoleRepository? _roleRepository;
    public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(UowDbContext);
    
    
    private IScheduleRepository? _scheduleRepository;
    public IScheduleRepository ScheduleRepository => _scheduleRepository ??= new ScheduleRepository(UowDbContext);
    
    
    private IStatusRepository? _statusRepository;
    public IStatusRepository StatusRepository => _statusRepository ??= new StatusRepository(UowDbContext);
    
    
    private ITaskRepository? _taskRepository;
    public ITaskRepository TaskRepository => _taskRepository ??= new TaskRepository(UowDbContext);
    
    
    private ITeamRepository? _teamRepository;
    public ITeamRepository TeamRepository => _teamRepository ??= new TeamRepository(UowDbContext);
    
    
    private ITicketRepository? _ticketRepository;
    public ITicketRepository TicketRepository => _ticketRepository ??= new TicketRepository(UowDbContext);
    
    
    private IUserInRoleRepository? _userInRoleRepository;
    public IUserInRoleRepository UserInRoleRepository => _userInRoleRepository ??= new UserInRoleRepository(UowDbContext);
    
    
    private IUserInTeamInTaskRepository? _userInTeamInTaskRepository;
    public IUserInTeamInTaskRepository UserInTeamInTaskRepository => _userInTeamInTaskRepository ??= new UserInTeamInTaskRepository(UowDbContext);


    private IUserInTeamRepository? _userInTeamRepository;
    public IUserInTeamRepository UserInTeamRepository => _userInTeamRepository ??= new UserInTeamRepository(UowDbContext);
    
    
    private IUserInWorkDayRepository? _userInWorkDayRepository;
    public IUserInWorkDayRepository UserInWorkDayRepository => _userInWorkDayRepository ??= new UserInWorkDayRepository(UowDbContext);
    
    
    private IWorkDayRepository? _workDayRepository;
    public IWorkDayRepository WorkDayRepository => _workDayRepository ??= new WorkDayRepository(UowDbContext);
}