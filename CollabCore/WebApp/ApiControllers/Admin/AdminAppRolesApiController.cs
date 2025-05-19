using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DTO.v1.ApiMappers.Admin;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace WebApp.ApiControllers.Admin
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    public class AdminAppRolesApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminAppRolesApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/admin/AdminRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Admin.AppRole>>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles.Select(AppRoleApiMapper.ToDto).ToList();
        }

        // GET: api/v1/admin/AdminRoles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Admin.AppRole>> GetRole(Guid id)
        {
            var appRole = await _context.Roles.FindAsync(id);

            if (appRole == null)
            {
                return NotFound();
            }

            return AppRoleApiMapper.ToDto(appRole);
        }

        // POST: api/v1/admin/AdminRoles
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Admin.AppRole>> CreateRole(App.DTO.v1.ApiEntities.Admin.AppRole dto)
        {
            var domain = AppRoleApiMapper.ToDomain(dto);
            domain.Id = Guid.NewGuid();

            _context.Roles.Add(domain);
            await _context.SaveChangesAsync();

            dto.Id = domain.Id;

            return CreatedAtAction(nameof(GetRole), new { id = dto.Id }, dto);
        }

        // PUT: api/v1/admin/AdminRoles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, App.DTO.v1.ApiEntities.Admin.AppRole dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var domain = await _context.Roles.FindAsync(id);
            if (domain == null)
            {
                return NotFound();
            }

            domain.Name = dto.RoleName;
            domain.NormalizedName = dto.RoleName.ToUpperInvariant();

            try
            {
                _context.Update(domain);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppRoleExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/v1/admin/AdminRoles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var appRole = await _context.Roles.FindAsync(id);
            if (appRole == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(appRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppRoleExists(Guid id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
