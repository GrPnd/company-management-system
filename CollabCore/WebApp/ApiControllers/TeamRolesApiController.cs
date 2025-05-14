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
    public class TeamRolesApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly TeamRoleApiMapper _mapper = new();

        /// <inheritdoc />
        public TeamRolesApiController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        /// <summary>
        /// Get all team roles.
        /// </summary>
        /// <returns>List of roles.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.TeamRole>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.TeamRole>>> GetRoles()
        {
            var data = await _bll.TeamRoleService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a team role by ID.
        /// </summary>
        /// <param name="id">TeamRole ID.</param>
        /// <returns>The requested role.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.TeamRole), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.TeamRole>> GetRole(Guid id)
        {
            var role = await _bll.TeamRoleService.FindAsync(id, User.GetUserId());

            if (role == null)
            {
                return NotFound();
            }

            return _mapper.Map(role)!;
        }

        
        /// <summary>
        /// Updates an existing teamRole.
        /// </summary>
        /// <param name="id">TeamRole ID.</param>
        /// <param name="teamRole">Updated teamRole data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutRole(Guid id, App.DTO.v1.ApiEntities.TeamRole teamRole)
        {
            if (id != teamRole.Id)
            {
                return BadRequest();
            }

            await _bll.TeamRoleService.UpdateAsync(_mapper.Map(teamRole)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        

        /// <summary>
        /// Creates a new team role.
        /// </summary>
        /// <param name="teamRole">TeamRole data.</param>
        /// <returns>The created teamRole with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.TeamRole), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.TeamRole>> PostRole(App.DTO.v1.ApiEntities.TeamRole teamRole)
        {
            var data = _mapper.Map(teamRole)!;
            _bll.TeamRoleService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetRole", new
            {
                id = teamRole.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, teamRole);
        }

        
        /// <summary>
        /// Deletes a team role by ID.
        /// </summary>
        /// <param name="id">TeamRole ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _bll.TeamRoleService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
