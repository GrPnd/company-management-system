﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.DAL.EF;
using App.Domain.Identity;
using App.DTO.v1.Identity;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers.Identity;

/// <summary>
/// User account controller - login, register, etc.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly Random _random = new Random();
    private readonly AppDbContext _context;

    private const string UserPassProblem = "User/Password problem";
    private const int RandomDelayMin = 500;
    private const int RandomDelayMax = 5000;

    private const string SettingsJWTPrefix = "JWTSecurity";
    private const string SettingsJWTKey = SettingsJWTPrefix + ":Key";
    private const string SettingsJWTIssuer = SettingsJWTPrefix + ":Issuer";
    private const string SettingsJWTAudience = SettingsJWTPrefix + ":Audience";
    private const string SettingsJWTExpiresInSeconds = SettingsJWTPrefix + ":ExpiresInSeconds";
    private const string SettingsJWTRefreshTokenExpiresInSeconds = SettingsJWTPrefix + ":RefreshTokenExpiresInSeconds";


    /// <summary>
    /// Constructor
    /// </summary>
    public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager, ILogger<AccountController> logger, AppDbContext context)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _context = context;
    }

    /// <summary>
    /// User authentication, returns JWT and refresh token
    /// </summary>
    /// <param name="loginInfo">Login model</param>
    /// <param name="jwtExpiresInSeconds">Optional, use custom jwt expiration</param>
    /// <param name="refreshTokenExpiresInSeconds">Optional, use custom refresh token expiration</param>
    /// <returns>JWT and refresh token</returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> Login(
        [FromBody]
        LoginInfo loginInfo,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {
        // verify user
        var appUser = await _userManager.FindByEmailAsync(loginInfo.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginInfo.Email);
            await Task.Delay(_random.Next(RandomDelayMin, RandomDelayMax));
            return NotFound(new Message(UserPassProblem));
        }

        // verify password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginInfo.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password {} for email {} was wrong", loginInfo.Password,
                loginInfo.Email);
            await Task.Delay(_random.Next(RandomDelayMin, RandomDelayMax));
            return NotFound(new Message(UserPassProblem));
        }


        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        
        var roles = await _userManager.GetRolesAsync(appUser);
        var identity = (ClaimsIdentity)claimsPrincipal.Identity!;
        foreach (var role in roles)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
        }
        
        
        if (!_context.Database.ProviderName!.Contains("InMemory"))
        {
            var deletedRows = await _context
                .RefreshTokens
                .Where(t => t.UserId == appUser.Id && t.Expiration < DateTime.UtcNow)
                .ExecuteDeleteAsync();
            _logger.LogInformation("Deleted {} refresh tokens", deletedRows);
        }
        

        var refreshToken = new AppRefreshToken()
        {
            UserId = appUser.Id,
            Expiration = GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJWTRefreshTokenExpiresInSeconds)
        };
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();


        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>(SettingsJWTKey)!,
            _configuration.GetValue<string>(SettingsJWTIssuer)!,
            _configuration.GetValue<string>(SettingsJWTAudience)!,
            GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds)
        );

        var responseData = new JWTResponse()
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken,
            UserId = appUser.Id,
            Email = appUser.Email,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            UserName = appUser.UserName,
            Roles = roles
        };

        return Ok(responseData);
    }


    /// <summary>
    /// Register new user, returns JWT and refresh token
    /// </summary>
    /// <param name="registerModel">Reg info</param>
    /// <param name="jwtExpiresInSeconds">Optional custom jwt expiration</param>
    /// <param name="refreshTokenExpiresInSeconds">Optional custom refresh token expiration</param>
    /// <returns></returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> Register(
        [FromBody]
        Register registerModel,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {
        var appUser = await _userManager.FindByEmailAsync(registerModel.Email);
        if (appUser != null)
        {
            _logger.LogWarning(" User {User} already registered", registerModel.Email);
            return BadRequest(new Message("User already registered"));
        }

        var refreshToken = new AppRefreshToken()
        {
            Expiration = GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJWTRefreshTokenExpiresInSeconds)
        };

        appUser = new AppUser()
        {
            Email = registerModel.Email,
            UserName = registerModel.Email,
            FirstName = registerModel.FirstName,
            LastName = registerModel.LastName,

            RefreshTokens = new List<AppRefreshToken>()
            {
                refreshToken
            }
        };
        var result = await _userManager.CreateAsync(appUser, registerModel.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("User {Email} created a new account with password", appUser.Email);

                await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.GivenName, appUser.FirstName));
                await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.Surname, appUser.LastName));
                await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.Name, appUser.UserName));

                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                
                var roles = await _userManager.GetRolesAsync(appUser);
                var identity = (ClaimsIdentity)claimsPrincipal.Identity!;
                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
                
                var jwt = IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration.GetValue<string>(SettingsJWTKey)!,
                    _configuration.GetValue<string>(SettingsJWTIssuer)!,
                    _configuration.GetValue<string>(SettingsJWTAudience)!,
                    GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds)
                );
                _logger.LogInformation("WebApi login. User {User}", registerModel.Email);
                return Ok(new JWTResponse()
                {
                    JWT = jwt,
                    RefreshToken = refreshToken.RefreshToken,
                    UserId = appUser.Id,
                    Email = appUser.Email,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    UserName = appUser.UserName,
                    Roles = roles
                });
        }

        var errors = result.Errors.Select(error => error.Description).ToList();
        return BadRequest(new Message() { Messages = errors });
    }

    /// <summary>
    /// Renew JWT using refresh token
    /// </summary>
    /// <param name="refreshTokenModel">Data for renewal</param>
    /// <param name="jwtExpiresInSeconds">Optional custom expiration for jwt</param>
    /// <param name="refreshTokenExpiresInSeconds">Optional custom expiration for refresh token</param>
    /// <returns></returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> RenewRefreshToken(
        [FromBody]
        RefreshTokenModel refreshTokenModel,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {
        _logger.LogInformation("RenewRefreshToken called with JWT: {Jwt}, RefreshToken: {RefreshToken}", refreshTokenModel.Jwt, refreshTokenModel.RefreshToken);
        JwtSecurityToken jwtToken;
        // get user info from jwt
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.Jwt);
            if (jwtToken == null)
            {
                _logger.LogWarning("RenewRefreshToken: No token found in input");
                return BadRequest(new Message("No token"));
            }
        }
        catch (Exception e)
        {
            _logger.LogWarning("RenewRefreshToken: Cant parse token: {Message}", e.Message);
            return BadRequest(new Message($"Cant parse the token, {e.Message}"));
        }

        // validate jwt, ignore expiration date
        if (!IdentityExtensions.ValidateJwt(
                refreshTokenModel.Jwt,
                _configuration.GetValue<string>(SettingsJWTKey)!,
                _configuration.GetValue<string>(SettingsJWTIssuer)!,
                _configuration.GetValue<string>(SettingsJWTAudience)!
            ))
        {
            _logger.LogWarning("RenewRefreshToken: JWT validation failed");
            return BadRequest("JWT validation fail");
        }

        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            _logger.LogWarning("RenewRefreshToken: No email claim found in JWT");
            return BadRequest(new Message("No email in jwt"));
        }

        // get user and tokens
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            _logger.LogWarning("RenewRefreshToken: User {Email} not found", userEmail);
            return NotFound($"User with email {userEmail} not found");
        }

        // load and compare refresh tokens

        var validRefreshTokens = await _context.Entry(appUser).Collection(u => u.RefreshTokens!)
            .Query()
            .Where(x =>
                (x.RefreshToken == refreshTokenModel.RefreshToken && x.Expiration > DateTime.UtcNow) ||
                (x.PreviousRefreshToken == refreshTokenModel.RefreshToken && x.PreviousExpiration > DateTime.UtcNow)
            )
            .ToListAsync();

        if (validRefreshTokens.Count == 0)
        {
            _logger.LogWarning("RenewRefreshToken: RefreshTokens collection is empty, no valid refresh tokens found");
            return BadRequest(new Message("Refresh token invalid or expired"));
        }

        if (validRefreshTokens.Count != 1)
        {
            _logger.LogWarning("RenewRefreshToken: More than one valid refresh token found");
            return Problem("More than one valid refresh token found.");
        }
        

        // generate new jwt
        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);

        // Log all claims for debugging
        foreach (var claim in claimsPrincipal.Claims)
        {
            _logger.LogInformation("Claim: {Type} = {Value}", claim.Type, claim.Value);
        }
        
        var roles = await _userManager.GetRolesAsync(appUser);
        var identity = (ClaimsIdentity)claimsPrincipal.Identity!;
        foreach (var role in roles)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
        }
        
        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>(SettingsJWTKey)!,
            _configuration.GetValue<string>(SettingsJWTIssuer)!,
            _configuration.GetValue<string>(SettingsJWTAudience)!,
            GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds)
        );
        
        _logger.LogInformation("RenewRefreshToken: Generated new JWT of length {Length}", jwt?.Length ?? 0);
        _logger.LogDebug("RenewRefreshToken: JWT token: {Jwt}", jwt);

        // make new refresh token, obsolete old ones
        var refreshToken = validRefreshTokens.First();
        if (refreshToken.RefreshToken == refreshTokenModel.RefreshToken)
        {
            refreshToken.PreviousRefreshToken = refreshToken.RefreshToken;
            refreshToken.PreviousExpiration = refreshToken.Expiration;

            refreshToken.RefreshToken = Guid.NewGuid().ToString();
            refreshToken.Expiration =
                GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJWTRefreshTokenExpiresInSeconds);

            await _context.SaveChangesAsync();
        }
        
        var res = new JWTResponse()
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken,
            UserId = appUser.Id,
            Email = appUser.Email,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            UserName = appUser.UserName,
            Roles = roles
        };

        return Ok(res);
    }

    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult> Logout([FromBody] LogoutInfo logout)
    {
        // delete the refresh token - so user is kicked out after jwt expiration
        // We do not invalidate the jwt on serverside - that would require pipeline modification and checking against db on every request
        // so client can actually continue to use the jwt until it expires (keep the jwt expiration time short ~1 min)

        var appUser = await _context.Users
            .Where(u => u.Id == User.GetUserId())
            .SingleOrDefaultAsync();
        if (appUser == null)
        {
            return NotFound(
                new Message(UserPassProblem)
            );
        }

        await _context.Entry(appUser)
            .Collection(u => u.RefreshTokens!)
            .Query()
            .Where(x =>
                (x.RefreshToken == logout.RefreshToken) ||
                (x.PreviousRefreshToken == logout.RefreshToken)
            )
            .ToListAsync();

        foreach (var appRefreshToken in appUser.RefreshTokens!)
        {
            _context.RefreshTokens.Remove(appRefreshToken);
        }

        var deleteCount = await _context.SaveChangesAsync();

        return Ok(new { TokenDeleteCount = deleteCount });
    }

    private DateTime GetExpirationDateTime(int? expiresInSeconds, string settingsKey)
    {
        if (expiresInSeconds <= 0) expiresInSeconds = int.MaxValue;
        expiresInSeconds = expiresInSeconds < _configuration.GetValue<int>(settingsKey)
            ? expiresInSeconds
            : _configuration.GetValue<int>(settingsKey);

        return DateTime.UtcNow.AddSeconds(expiresInSeconds ?? 60);
    }
    
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<ActionResult<Message>> UpdateProfile([FromBody] UpdateUserProfile dto)
    {
        var userId = User.GetUserId();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null) return NotFound(new Message("User not found"));

        if (dto.Address != null)
        {
            user.Address = dto.Address;
        }
        
        if (dto.PhoneNumber != null)
        {
            user.PhoneNumber = dto.PhoneNumber;
        }
        
        if (dto.AdditionalInfo != null)
        {
            user.AdditionalInfo = dto.AdditionalInfo;
        }
        
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(new Message
            {
                Messages = result.Errors.Select(e => e.Description).ToList()
            });
        }

        return Ok(new Message("Profile updated successfully"));
    }
    
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [ProducesResponseType(typeof(UpdateUserProfile), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateUserProfile>> GetProfile()
    {
        var userId = User.GetUserId();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return NotFound(new Message("User not found"));

        var profile = new UpdateUserProfile
        {
            Address = user.Address,
            PhoneNumber = user.PhoneNumber,
            AdditionalInfo = user.AdditionalInfo
        };

        return Ok(profile);
    }
    
    
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Message>> ChangePassword([FromBody] ChangePassword model)
    {
        var userId = User.GetUserId();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return BadRequest(new Message("User not found."));
        }

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (!result.Succeeded)
        {
            return BadRequest(new Message
            {
                Messages = result.Errors.Select(e => e.Description).ToList()
            });
        }

        return Ok(new Message("Password changed successfully."));
    }
}
