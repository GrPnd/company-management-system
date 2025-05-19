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
public class AdminAppUsersApiController : ControllerBase
{
     private readonly AppDbContext _context;

    public AdminAppUsersApiController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/v1/admin/AdminAppUsers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Admin.AppUser>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users.Select(AppUserApiMapper.ToDto).ToList();
    }

    // GET: api/v1/admin/AdminAppUsers/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<App.DTO.v1.ApiEntities.Admin.AppUser>> GetUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return AppUserApiMapper.ToDto(user);
    }

    // POST: api/v1/admin/AdminAppUsers
    [HttpPost]
    public async Task<ActionResult<App.DTO.v1.ApiEntities.Admin.AppUser>> CreateUser(App.DTO.v1.ApiEntities.Admin.AppUser dto)
    {
        var user = AppUserApiMapper.ToDomain(dto);
        user.Id = Guid.NewGuid();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        dto.Id = user.Id;

        return CreatedAtAction(nameof(GetUser), new { id = dto.Id }, dto);
    }

    // PUT: api/v1/admin/AdminAppUsers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, App.DTO.v1.ApiEntities.Admin.AppUser dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("ID mismatch.");
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.UserName = dto.Email;
        user.NormalizedUserName = dto.Email.ToUpperInvariant();
        user.NormalizedEmail = dto.Email.ToUpperInvariant();

        _context.Update(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/v1/admin/AdminAppUsers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(Guid id)
    {
        return _context.Users.Any(u => u.Id == id);
    }
}