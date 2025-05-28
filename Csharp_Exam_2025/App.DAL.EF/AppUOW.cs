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
    
    private IPersonRepository? _personRepository;
    public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(UowDbContext);
    
    
    private ITeamRepository? _teamRepository;
    public ITeamRepository TeamRepository =>
        _teamRepository ??= new TeamRepository(UowDbContext);
    
    
    private IUserInTeamRepository? _userInTeamRepository;
    public IUserInTeamRepository UserInTeamRepository =>
        _userInTeamRepository ??= new UserInTeamRepository(UowDbContext);

}