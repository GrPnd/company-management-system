using System.Net;
using System.Net.Http.Json;
using App.DTO.v1.ApiEntities;
using App.DTO.v1.ApiEntities.Admin;
using App.DTO.v1.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace App.Tests.Integration.Api;

[Collection("Sequential")]
public class HappyFlowTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;


    public HappyFlowTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }


    [Fact]
    public async Task AdminCreateAdminPerson()
    {
        // Arrange
        var (_, admin) = await LoginAsAdminAsync();

        var personAddObject = new Person()
        {
            PersonName = "Admin",
        };

        // Act
        var postResponse = await _client.PostAsJsonAsync($"/api/v1/PersonsApi/user/{admin.Id}", personAddObject);

        // Assert
        postResponse.EnsureSuccessStatusCode();
        Assert.NotNull(postResponse);
        var createdPerson = await postResponse.Content.ReadFromJsonAsync<Person>();
        Assert.NotNull(createdPerson);


        // Act
        var personGetResponse = await _client.GetAsync($"/api/v1/PersonsApi/user/{admin.Id}");

        // Assert
        personGetResponse.EnsureSuccessStatusCode();
        var personGetObject = await personGetResponse.Content.ReadFromJsonAsync<Person>();
        Assert.NotNull(personGetObject);
        Assert.NotEqual(Guid.Empty, personGetObject.Id);
        Assert.Equal(personAddObject.PersonName, personGetObject.PersonName);
    }


    [Fact]
    public async Task UserCreatesAccount_AdminCreatesPersonForUser_AdminCreatesTeam_PutsPersonToTeam()
    {
        // Arrange
        var registrationData = new Register()
        {
            Email = "test@test.ee",
            FirstName = "Tester",
            LastName = "Sester",
            Password = "Password.123"
        };

        // Act
        var registerResponse = await _client.PostAsJsonAsync("/api/v1/account/register", registrationData);

        // Assert
        registerResponse.EnsureSuccessStatusCode();
        var registerResponseData = await registerResponse.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(registerResponseData);
        Assert.True(registerResponseData.JWT.Length > 128);


        // Arrange
        await LoginAsAdminAsync();

        var createdPerson = await CreatePersonForUserAsync(
            registerResponseData.UserId, registrationData.FirstName + " " + registrationData.LastName);


        // Arrange
        var department = await CreateGetDepartment("IT Department");
        var team = await CreateGetTeam("IT Team", department.Id);
        var workerTeamRole = await CreateGetTeamRole("Worker");

        var userInTeamAddObject = new UserInTeam()
        {
            Since = DateTime.Now.ToUniversalTime(),
            Until = null,
            TeamRoleId = workerTeamRole.Id,
            UserId = createdPerson.Id,
            TeamId = team.Id
        };

        // Act
        var userInTeamResponse = await _client.PostAsJsonAsync("/api/v1/UsersInTeamsApi", userInTeamAddObject);

        // Assert
        userInTeamResponse.EnsureSuccessStatusCode();
        var userInTeamResponseData = await userInTeamResponse.Content.ReadFromJsonAsync<UserInTeam>();
        Assert.NotNull(userInTeamResponseData);
        Assert.Equal(userInTeamResponseData.TeamId, team.Id);
        Assert.Equal(userInTeamResponseData.UserId, createdPerson.Id);
        Assert.Equal(userInTeamResponseData.TeamRoleId, workerTeamRole.Id);
    }


    [Fact]
    public async Task TeamLeaderCreatesTask_AssignsToWorker_WorkerUpdatesStatus()
    {
        // Arrange
        var registrationDataWorker = new Register()
        {
            Email = "worker@test.ee",
            FirstName = "Worker",
            LastName = "One",
            Password = "Password.123"
        };
        var registrationDataLeader = new Register()
        {
            Email = "leader@test.ee",
            FirstName = "Leader",
            LastName = "Two",
            Password = "Password.123"
        };

        // Act
        var registerResponseWorker = await _client.PostAsJsonAsync("/api/v1/account/register", registrationDataWorker);
        var registerResponseLeader = await _client.PostAsJsonAsync("/api/v1/account/register", registrationDataLeader);

        // Assert
        registerResponseWorker.EnsureSuccessStatusCode();
        registerResponseLeader.EnsureSuccessStatusCode();

        var jwtWorker = await registerResponseWorker.Content.ReadFromJsonAsync<JWTResponse>();
        var jwtLeader = await registerResponseLeader.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(jwtWorker);
        Assert.NotNull(jwtLeader);

        // Arrange
        var (_, admin) = await LoginAsAdminAsync();

        // Act
        var personWorker = await CreatePersonForUserAsync(jwtWorker.UserId,
            $"{registrationDataWorker.FirstName} {registrationDataWorker.LastName}");
        var personLeader = await CreatePersonForUserAsync(jwtLeader.UserId,
            $"{registrationDataLeader.FirstName} {registrationDataLeader.LastName}");

        // Assert
        Assert.NotNull(personWorker);
        Assert.NotEqual(personWorker.Id, Guid.Empty);
        Assert.NotNull(personLeader);
        Assert.NotEqual(personLeader.Id, Guid.Empty);
        

        // Arrange
        var department = await CreateGetDepartment("Engineering");
        var team = await CreateGetTeam("Engineering Team", department.Id);
        var workerRole = await CreateGetTeamRole("Worker");
        var leaderRole = await CreateGetTeamRole("Team Leader");
        
        
        var userInTeamWorker = new UserInTeam
        {
            Since = DateTime.Now.ToUniversalTime(),
            Until = null,
            TeamRoleId = workerRole.Id,
            UserId = personWorker.Id,
            TeamId = team.Id
        };
        var userInTeamLeader = new UserInTeam
        {
            Since = DateTime.Now.ToUniversalTime(),
            Until = null,
            TeamRoleId = leaderRole.Id,
            UserId = personLeader.Id,
            TeamId = team.Id
        };

        // Act
        var userInTeamWorkerResponse = await _client.PostAsJsonAsync("/api/v1/UsersInTeamsApi", userInTeamWorker);
        var userInTeamLeaderResponse = await _client.PostAsJsonAsync("/api/v1/UsersInTeamsApi", userInTeamLeader);
        
        // Assert
        userInTeamWorkerResponse.EnsureSuccessStatusCode();
        userInTeamLeaderResponse.EnsureSuccessStatusCode();

        // Act
        var allUserWorkers = await _client.GetAsync("/api/v1/UsersInTeamsApi");
        var allUserLeaders = await _client.GetAsync("/api/v1/UsersInTeamsApi");
        
        // Assert
        allUserWorkers.EnsureSuccessStatusCode();
        allUserLeaders.EnsureSuccessStatusCode();
        
        var allUserWorkersData = await allUserWorkers.Content.ReadFromJsonAsync<List<UserInTeam>>();
        var allUserLeadersData = await allUserLeaders.Content.ReadFromJsonAsync<List<UserInTeam>>();
        
        var userWorker = allUserWorkersData!.FirstOrDefault(u => u.UserId == personWorker.Id);
        var userLeader = allUserLeadersData!.FirstOrDefault(u => u.UserId == personLeader.Id);
        
        Assert.NotNull(userWorker);
        Assert.NotEqual(userWorker.Id, Guid.Empty);
        Assert.NotNull(userLeader);
        Assert.NotEqual(userLeader.Id, Guid.Empty);

        // Arrange
        var statusAssigned = await CreateGetStatus("Assigned");
        var statusCompleting = await CreateGetStatus("Completing");

        var taskToCreate = new App.DTO.v1.ApiEntities.Task
        {
            Name = "Complete Report",
            Description = "Complete the quarterly report",
            AssignedAt = DateTime.Now.ToUniversalTime(),
            Deadline = DateTime.Now.AddDays(7).ToUniversalTime(),
            StatusId = statusAssigned.Id
        };

        // Act
        // Team Leader creates the task
        var taskCreateResponse = await _client.PostAsJsonAsync("/api/v1/TasksApi", taskToCreate);

        // Assert
        taskCreateResponse.EnsureSuccessStatusCode();
        var createdTask = await taskCreateResponse.Content.ReadFromJsonAsync<App.DTO.v1.ApiEntities.Task>();
        Assert.NotNull(createdTask);
        Assert.NotEqual(createdTask.Id, Guid.Empty);

        // Arrange
        var userInTeamInTask = new UserInTeamInTask
        {
            Since = DateTime.Now.ToUniversalTime(),
            Until = DateTime.Now.AddDays(7).ToUniversalTime(),
            TaskId = createdTask.Id,
            UserInTeamId = userWorker.Id
        };

        // Act
        
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtLeader.JWT);
        // Team Leader assigns task to Worker via UserInTeamInTask
        var assignTaskResponse = await _client.PostAsJsonAsync("/api/v1/UsersInTeamsInTasksApi", userInTeamInTask);

        // Assert

        assignTaskResponse.EnsureSuccessStatusCode();
        var assignedUserInTeamInTask = await assignTaskResponse.Content.ReadFromJsonAsync<UserInTeamInTask>();
        Assert.NotNull(assignedUserInTeamInTask);
        Assert.Equal(userWorker.Id, assignedUserInTeamInTask.UserInTeamId);
        Assert.Equal(createdTask.Id, assignedUserInTeamInTask.TaskId);

        
        // Arrange
        // Worker updates task status to "Completing"
        createdTask.StatusId = statusCompleting.Id;

        // Simulate authorization as Worker
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtWorker.JWT);
        
        // Act
        var updateTaskResponse = await _client.PutAsJsonAsync($"/api/v1/TasksApi/{createdTask.Id}", createdTask);
        
        // Assert
        updateTaskResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.NoContent, updateTaskResponse.StatusCode);
        
        // Act
        var updatedTaskResponse = await _client.GetAsync($"/api/v1/TasksApi/{createdTask.Id}");
        
        // Assert
        updateTaskResponse.EnsureSuccessStatusCode();
        var updatedTask = await updatedTaskResponse.Content.ReadFromJsonAsync<App.DTO.v1.ApiEntities.Task>();
        Assert.NotNull(updatedTask);
        
        Assert.NotEqual(updatedTask.StatusId, statusAssigned.Id);
        Assert.Equal(updatedTask.StatusId, statusCompleting.Id);
    }

    
    
    private async Task<Status> CreateGetStatus(string statusName)
    {
        // Arrange
        var statusAddObject = new Status
        {
            Name = statusName,
        };

        // Act
        var postResponse = await _client.PostAsJsonAsync("/api/v1/StatusesApi", statusAddObject);

        // Assert
        postResponse.EnsureSuccessStatusCode();
        var createdStatus = await postResponse.Content.ReadFromJsonAsync<Status>();
        Assert.NotNull(createdStatus);

        // Act
        var getResponse = await _client.GetAsync("/api/v1/StatusesApi");

        // Assert
        getResponse.EnsureSuccessStatusCode();
        var statuses = await getResponse.Content.ReadFromJsonAsync<List<Status>>();
        Assert.NotNull(statuses);

        var status = statuses.FirstOrDefault(s => s.Name == statusName);
        Assert.NotNull(status);

        return status;
    }


    private async Task<(JWTResponse jwt, AppUser admin)> LoginAsAdminAsync()
    {
        // Arrange
        var loginData = new LoginInfo
        {
            Email = "admin@cc.ee",
            Password = "Foo.Bar.1"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var jwtResponse = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(jwtResponse);


        // Arrange
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtResponse.JWT);

        // Act
        var getResponse = await _client.GetAsync("/api/v1/admin/AdminAppUsersApi");

        // Assert
        getResponse.EnsureSuccessStatusCode();
        var adminUsers = await getResponse.Content.ReadFromJsonAsync<List<AppUser>>();
        Assert.NotNull(adminUsers);
        Assert.NotEmpty(adminUsers);
        var admin = adminUsers.FirstOrDefault(u => u.Email == loginData.Email);
        Assert.NotNull(admin);

        return (jwtResponse, admin);
    }

    private async Task<Person> CreatePersonForUserAsync(Guid userId, string personName)
    {
        // Arrange
        var personAddObject = new Person
        {
            PersonName = personName,
        };

        // Act
        var postResponse = await _client.PostAsJsonAsync($"/api/v1/PersonsApi/user/{userId}", personAddObject);

        // Assert
        postResponse.EnsureSuccessStatusCode();
        var createdPerson = await postResponse.Content.ReadFromJsonAsync<Person>();
        Assert.NotNull(createdPerson);
        
        
        // Act
        var personGetResponse = await _client.GetAsync($"/api/v1/PersonsApi/user/{userId}");

        // Assert
        personGetResponse.EnsureSuccessStatusCode();
        var personGetObject = await personGetResponse.Content.ReadFromJsonAsync<Person>();
        Assert.NotNull(personGetObject);
        Assert.NotEqual(Guid.Empty, personGetObject.Id);
        Assert.Equal(createdPerson.PersonName, personGetObject.PersonName);


        return personGetObject;
    }

    private async Task<Department> CreateGetDepartment(string departmentName)
    {
        // Arrange
        var departmentAddObject = new Department
        {
            Name = departmentName,
        };

        // Act
        var postResponse = await _client.PostAsJsonAsync($"/api/v1/DepartmentsApi", departmentAddObject);

        // Assert
        postResponse.EnsureSuccessStatusCode();
        var createdDepartment = await postResponse.Content.ReadFromJsonAsync<Department>();
        Assert.NotNull(createdDepartment);


        // Act
        var allDepartmentsResponse = await _client.GetAsync("/api/v1/DepartmentsApi");

        // Assert
        allDepartmentsResponse.EnsureSuccessStatusCode();
        var allDepartments = await allDepartmentsResponse.Content.ReadFromJsonAsync<List<Department>>();
        Assert.NotNull(allDepartments);


        var department = allDepartments.FirstOrDefault(d => d.Name == departmentName);
        Assert.NotNull(department);
        Assert.NotEqual(Guid.Empty, department.Id);

        return department;
    }

    private async Task<Team> CreateGetTeam(string teamName, Guid departmentId)
    {
        // Arrange
        var teamAddObject = new Team()
        {
            Name = teamName,
            DepartmentId = departmentId
        };

        // Act
        var postResponse = await _client.PostAsJsonAsync($"/api/v1/TeamsApi", teamAddObject);

        // Assert
        postResponse.EnsureSuccessStatusCode();
        var createdTeam = await postResponse.Content.ReadFromJsonAsync<Team>();
        Assert.NotNull(createdTeam);


        // Act
        var allTeamsResponse = await _client.GetAsync("/api/v1/TeamsApi");

        // Assert
        allTeamsResponse.EnsureSuccessStatusCode();
        var allTeams = await allTeamsResponse.Content.ReadFromJsonAsync<List<Team>>();
        Assert.NotNull(allTeams);


        var team = allTeams.FirstOrDefault(d => d.Name == teamName);
        Assert.NotNull(team);
        Assert.NotEqual(Guid.Empty, team.Id);

        return team;
    }

    private async Task<TeamRole> CreateGetTeamRole(string teamRoleName)
    {
        // Arrange
        var teamRoleAddObject = new TeamRole()
        {
            Name = teamRoleName
        };

        // Act
        var postResponse = await _client.PostAsJsonAsync($"/api/v1/TeamRolesApi", teamRoleAddObject);

        // Assert
        postResponse.EnsureSuccessStatusCode();
        var createdTeamRole = await postResponse.Content.ReadFromJsonAsync<TeamRole>();
        Assert.NotNull(createdTeamRole);


        // Act
        var getResponse = await _client.GetAsync("/api/v1/TeamRolesApi");


        // Assert
        getResponse.EnsureSuccessStatusCode();
        var teamRoles = await getResponse.Content.ReadFromJsonAsync<List<TeamRole>>();
        Assert.NotNull(teamRoles);
        Assert.NotEmpty(teamRoles);

        var teamRole = teamRoles.FirstOrDefault(t => t.Name == teamRoleName);
        Assert.NotNull(teamRole);

        return teamRole;
    }
}