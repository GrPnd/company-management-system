using App.BLL.Contracts.Services;
using App.BLL.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using Base.BLL.Contracts;
using Moq;
using Task = System.Threading.Tasks.Task;
using TeamRole = App.BLL.DTO.TeamRole;
using UserInTeam = App.BLL.DTO.UserInTeam;

namespace App.Tests.Unit;

public class CustomServiceTest
{
    private readonly Mock<IAppUOW> _uowMock;
    private readonly Mock<IUserInTeamRepository> _repoMock;
    private readonly Mock<IBLLMapper<UserInTeam, App.DAL.DTO.UserInTeam>> _mapperMock;
    private readonly Mock<ITeamRoleService> _teamRoleServiceMock;
    private readonly UserInTeamService _baseService;

    public CustomServiceTest()
    {
        _repoMock = new Mock<IUserInTeamRepository>();
        _uowMock = new Mock<IAppUOW>();
        _mapperMock = new Mock<IBLLMapper<UserInTeam, App.DAL.DTO.UserInTeam>>();
        _teamRoleServiceMock = new Mock<ITeamRoleService>();

        _uowMock.Setup(u => u.UserInTeamRepository).Returns(_repoMock.Object);

        _baseService = new UserInTeamService(
            _uowMock.Object,
            _mapperMock.Object,
            _teamRoleServiceMock.Object
        );
    }


    [Fact]
    public async Task GetUserInTeamByPersonId_ReturnsMappedResults()
    {
        // Arrange
        var personId = Guid.NewGuid();

        var dalData = new List<App.DAL.DTO.UserInTeam>
        {
            new() { Id = Guid.NewGuid(), UserId = personId },
            new() { Id = Guid.NewGuid(), UserId = personId },
            new() { Id = Guid.NewGuid(), UserId = Guid.NewGuid() }
        };

        var filteredDalData = dalData.Where(d => d.UserId == personId).ToList();
        var mappedBllData = filteredDalData.Select(d => new UserInTeam { Id = d.Id }).ToList();

        _repoMock.Setup(r => r.GetUserInTeamByPersonId(personId)).ReturnsAsync(filteredDalData);

        foreach (var dal in filteredDalData)
        {
            var bll = mappedBllData.First(m => m.Id == dal.Id);
            _mapperMock.Setup(m => m.Map(dal)).Returns(bll);
        }

        // Act
        var result = (await _baseService.GetUserInTeamByPersonId(personId)).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, item => Assert.NotNull(item));
        Assert.All(result, item => Assert.Equal(personId, filteredDalData.First(d => d.Id == item!.Id).UserId));
        Assert.NotEmpty(result);
    }
    
    
    [Fact]
    public async Task GetTeamLeadersInTeamByTeamId_ReturnsEmpty_WhenRoleNotFound()
    {
        // Arrange
        _teamRoleServiceMock.Setup(t => t.AllAsync(default)).ReturnsAsync(new List<TeamRole>());

        // Act
        var result = await _baseService.GetTeamLeadersInTeamByTeamId(Guid.NewGuid());

        // Assert
        Assert.Empty(result);
    }


    [Fact]
    public async Task GetTeamLeadersInTeamByTeamId_ReturnsFilteredMappedResults()
    {
        // Arrange
        var teamId = Guid.NewGuid();
        var teamLeaderRoleId = Guid.NewGuid();
        var workerRoleId = Guid.NewGuid();

        _teamRoleServiceMock.Setup(t => t.AllAsync(default)).ReturnsAsync(new List<TeamRole>
        {
            new() { Id = teamLeaderRoleId, Name = "Team Leader" },
            new() { Id = workerRoleId, Name = "Worker" }
        });

        var matchingDal = new App.DAL.DTO.UserInTeam
        {
            Id = Guid.NewGuid(),
            TeamId = teamId,
            TeamRoleId = teamLeaderRoleId
        };

        var notMatchingDal = new App.DAL.DTO.UserInTeam
        {
            Id = Guid.NewGuid(),
            TeamId = teamId,
            TeamRoleId = workerRoleId
        };

        var dalData = new List<App.DAL.DTO.UserInTeam>
        {
            matchingDal, notMatchingDal
        };

        _repoMock.Setup(r => r.AllAsync(default)).ReturnsAsync(dalData);
        _mapperMock.Setup(m => m.Map(matchingDal)).Returns(new UserInTeam { Id = matchingDal.Id });

        // Act
        var result = (await _baseService.GetTeamLeadersInTeamByTeamId(teamId)).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(matchingDal.Id, result[0]!.Id);
        Assert.NotEqual(notMatchingDal.Id, result[0]!.Id);
        Assert.NotEmpty(result);
    }
    
    
    [Fact]
    public async Task GetAllEnrichedUsersInTeam_ReturnsAllMappedResults()
    {
        // Arrange
        var dalUsers = new List<App.DAL.DTO.UserInTeam>
        {
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.NewGuid() }
        };

        var bllUsers = dalUsers.Select(d => new UserInTeam { Id = d.Id }).ToList();

        _repoMock.Setup(r => r.GetAllEnrichedUsersInTeam()).ReturnsAsync(dalUsers);

        foreach (var dal in dalUsers)
        {
            var mapped = bllUsers.First(b => b.Id == dal.Id);
            _mapperMock.Setup(m => m.Map(It.Is<App.DAL.DTO.UserInTeam>(d => d.Id == dal.Id)))
                .Returns(mapped);
        }

        // Act
        var result = (await _baseService.GetAllEnrichedUsersInTeam()).ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bllUsers.Count, result.Count);
        Assert.All(result, item => Assert.Contains(bllUsers, expected => expected.Id == item!.Id));

        _repoMock.Verify(r => r.GetAllEnrichedUsersInTeam(), Times.Once);
        _mapperMock.Verify(m => m.Map(It.IsAny<App.DAL.DTO.UserInTeam>()), Times.Exactly(dalUsers.Count));
    }

    
    [Fact]
    public async Task GetEnrichedUsersInTeam_ReturnsMappedResults_FilteringHappensInRepository()
    {
        // Arrange
        var teamId = Guid.NewGuid();

        var validDal1 = new App.DAL.DTO.UserInTeam
        {
            Id = Guid.NewGuid(),
            TeamId = teamId,
            UserId = Guid.NewGuid(),
            User = new App.DAL.DTO.Person { Id = Guid.NewGuid(), PersonName = "Toomas" },
            Team = new App.DAL.DTO.Team { Id = teamId, Name = "Alpha Team" },
            TeamRole = new App.DAL.DTO.TeamRole { Id = Guid.NewGuid(), Name = "Worker" },
            TeamRoleId = Guid.NewGuid(),
            Since = DateTime.UtcNow
        };

        var validDal2 = new App.DAL.DTO.UserInTeam
        {
            Id = Guid.NewGuid(),
            TeamId = teamId,
            UserId = Guid.NewGuid(),
            User = new App.DAL.DTO.Person { Id = Guid.NewGuid(), PersonName = "Peeter" },
            Team = new App.DAL.DTO.Team { Id = teamId, Name = "Alpha Team" },
            TeamRole = new App.DAL.DTO.TeamRole { Id = Guid.NewGuid(), Name = "Team Leader" },
            TeamRoleId = Guid.NewGuid(),
            Since = DateTime.UtcNow
        };
        
        var invalidDal = new App.DAL.DTO.UserInTeam
        {
            Id = Guid.NewGuid(),
            TeamId = Guid.NewGuid(), // different teamId
            User = new App.DAL.DTO.Person { Id = Guid.NewGuid(), PersonName = "Joonas" },
            Team = new App.DAL.DTO.Team { Id = Guid.NewGuid(), Name = "Beta Team" },
            TeamRole = new App.DAL.DTO.TeamRole { Id = Guid.NewGuid(), Name = "Team Leader" }
        };

        var dalData = new List<App.DAL.DTO.UserInTeam> { validDal1, validDal2 };
        var mappedBll1 = new UserInTeam
        {
            Id = validDal1.Id,
            TeamId = validDal1.TeamId,
            UserId = validDal1.UserId
        };

        var mappedBll2 = new UserInTeam
        {
            Id = validDal2.Id,
            TeamId = validDal2.TeamId,
            UserId = validDal2.UserId
        };

        _repoMock.Setup(r => r.GetEnrichedUsersInTeam(teamId)).ReturnsAsync(dalData);

        _mapperMock.Setup(m => m.Map(It.Is<App.DAL.DTO.UserInTeam>(u => u.Id == validDal1.Id)))
            .Returns(mappedBll1);
        _mapperMock.Setup(m => m.Map(It.Is<App.DAL.DTO.UserInTeam>(u => u.Id == validDal2.Id)))
            .Returns(mappedBll2);

        // Act
        var result = (await _baseService.GetEnrichedUsersInTeam(teamId)).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, r => r?.Id == validDal1.Id);
        Assert.Contains(result, r => r?.Id == validDal2.Id);
        Assert.DoesNotContain(result, r => r?.Id == invalidDal.Id);
        Assert.DoesNotContain(result, r => r == null);

        _mapperMock.Verify(m => m.Map(It.IsAny<App.DAL.DTO.UserInTeam>()), Times.Exactly(2));
    }
}