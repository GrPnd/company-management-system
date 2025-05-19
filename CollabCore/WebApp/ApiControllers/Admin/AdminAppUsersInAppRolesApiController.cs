using App.DAL.EF;
using App.DTO.v1.ApiMappers.Admin;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers.Admin;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/admin/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = "admin")]
public class AdminAppUsersInAppRolesApiController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminAppUsersInAppRolesApiController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/v1/admin/AdminAppUserRoles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Admin.AppUserInAppRole>>> GetUserRoles()
    {
        var roles = await _context.UserRoles.ToListAsync();
        return roles.Select(AppUserInAppRoleApiMapper.ToDto).ToList();
    }

    // GET: api/v1/admin/AdminAppUserRoles/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<App.DTO.v1.ApiEntities.Admin.AppUserInAppRole>> GetUserRole(Guid id)
    {
        var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.Id == id);

        if (userRole == null)
        {
            return NotFound();
        }

        return AppUserInAppRoleApiMapper.ToDto(userRole);
    }

    // POST: api/v1/admin/AdminAppUserRoles
    [HttpPost]
    public async Task<ActionResult<App.DTO.v1.ApiEntities.Admin.AppUserInAppRole>> CreateUserRole(App.DTO.v1.ApiEntities.Admin.AppUserInAppRole dto)
    {
        var entity = AppUserInAppRoleApiMapper.ToDomain(dto);
        entity.Id = Guid.NewGuid();

        _context.UserRoles.Add(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;

        return CreatedAtAction(nameof(GetUserRole), new { id = dto.Id }, dto);
    }

    // PUT: api/v1/admin/AdminAppUserRoles/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserRole(Guid id, App.DTO.v1.ApiEntities.Admin.AppUserInAppRole dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("ID mismatch.");
        }

        var entity = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.Id == id);
        if (entity == null)
        {
            return NotFound();
        }

        entity.UserId = dto.AppUserId;
        entity.RoleId = dto.AppRoleId;

        _context.UserRoles.Update(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/v1/admin/AdminAppUserRoles/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserRole(Guid id)
    {
        var entity = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.Id == id);
        if (entity == null)
        {
            return NotFound();
        }

        _context.UserRoles.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserRoleExists(Guid id)
    {
        return _context.UserRoles.Any(e => e.Id == id);
    }
}