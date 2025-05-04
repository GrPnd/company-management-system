using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.DTO.v1.ApiMappers;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <inheritdoc />
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolesApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RoleApiMapper _mapper = new();

        /// <inheritdoc />
        public RolesApiController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns>List of roles.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Role>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Role>>> GetRoles()
        {
            var data = await _bll.RoleService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a role by ID.
        /// </summary>
        /// <param name="id">Role ID.</param>
        /// <returns>The requested role.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Role), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Role>> GetRole(Guid id)
        {
            var role = await _bll.RoleService.FindAsync(id, User.GetUserId());

            if (role == null)
            {
                return NotFound();
            }

            return _mapper.Map(role)!;
        }

        
        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="id">Role ID.</param>
        /// <param name="role">Updated role data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutRole(Guid id, App.DTO.v1.ApiEntities.Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            await _bll.RoleService.UpdateAsync(_mapper.Map(role)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        

        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="role">Role data.</param>
        /// <returns>The created role with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Role), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Role>> PostRole(App.DTO.v1.ApiEntities.Role role)
        {
            var data = _mapper.Map(role)!;
            _bll.RoleService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetRole", new
            {
                id = role.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, role);
        }

        
        /// <summary>
        /// Deletes a role by ID.
        /// </summary>
        /// <param name="id">Role ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _bll.RoleService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
