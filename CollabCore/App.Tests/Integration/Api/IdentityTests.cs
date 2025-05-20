using System.Net;
using System.Net.Http.Json;
using App.DTO.v1.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;


namespace App.Tests.Integration.Api;

[Collection("Sequential")]
public class IdentityTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;


    public IdentityTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }


    [Fact]
    public async Task Register_New_User()
    {
        // Arrange
        var registrationData = new Register()
        {
            Email = "test@test.ee",
            FirstName = "Test",
            LastName = "User",
            Password = "Password.123"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/register", registrationData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
    }


    [Fact]
    public async Task Login_Existing_User()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "admin@cc.ee",
            Password = "Foo.Bar.1"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
    }

    
    [Fact]
    public async Task Login_Existing_User_Check_Rights()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "admin@cc.ee",
            Password = "Foo.Bar.1"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);


        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);

        var getResponse = await _client.GetAsync("/api/v1/DepartmentsApi");
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task No_Bearer_Header_Unauthorized()
    {
        var getResponse = await _client.GetAsync("/api/v1/DepartmentsApi");
        Assert.Equal(HttpStatusCode.Unauthorized, getResponse.StatusCode);
    }

    [Fact]
    public async Task JWT_Custom_Expiration()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "admin@cc.ee",
            Password = "Foo.Bar.1"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login?jwtExpiresInSeconds=1", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);


        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);

        var getResponse = await _client.GetAsync("/api/v1/DepartmentsApi");
        getResponse.EnsureSuccessStatusCode();


        // Wait for JWT to expire
        await Task.Delay(1500);
        var getResponseAuthExpired = await _client.GetAsync("/api/v1/DepartmentsApi");

        Assert.Equal(HttpStatusCode.Unauthorized, getResponseAuthExpired.StatusCode);
    }


    [Fact]
    public async Task JWT_Refresh()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "admin@cc.ee",
            Password = "Foo.Bar.1"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login?jwtExpiresInSeconds=1", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);


        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);

        var getResponse = await _client.GetAsync("/api/v1/DepartmentsApi");
        getResponse.EnsureSuccessStatusCode();


        // Wait for JWT to expire
        await Task.Delay(1500);

        var getResponseAuthExpired = await _client.GetAsync("/api/v1/DepartmentsApi");

        Assert.Equal(HttpStatusCode.Unauthorized, getResponseAuthExpired.StatusCode);

        // Refresh JWT
        var refreshResponse = await _client.PostAsJsonAsync("/api/v1/account/RenewRefreshToken", new RefreshTokenModel()
        {
            Jwt = responseData.JWT,
            RefreshToken = responseData.RefreshToken
        });
        
        
        var refreshedResponseData = await refreshResponse.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(refreshedResponseData);
        
        
        Assert.True(refreshedResponseData.JWT?.Length > 128);
        
        
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", refreshedResponseData.JWT);

        var getResponse2 = await _client.GetAsync("/api/v1/DepartmentsApi");
        getResponse2.EnsureSuccessStatusCode();
    }
}