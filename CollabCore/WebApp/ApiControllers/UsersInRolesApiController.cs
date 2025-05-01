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
    [Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersInRolesApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserInRoleApiMapper _mapper;

        /// <inheritdoc />
        public UsersInRolesApiController(IAppBLL bll, UserInRoleApiMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        
        /// <summary>
        /// Get all users in roles.
        /// </summary>
        /// <returns>List of users in roles.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.UserInRole>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.UserInRole>>> GetUsersInRoles()
        {
            var data = await _bll.UserInRoleService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a user in role by ID.
        /// </summary>
        /// <param name="id">User in role ID.</param>
        /// <returns>The requested user in role.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.UserInRole), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.UserInRole>> GetUserInRole(Guid id)
        {
            var userInRole = await _bll.UserInRoleService.FindAsync(id, User.GetUserId());

            if (userInRole == null)
            {
                return NotFound();
            }

            return _mapper.Map(userInRole);
        }

        
        /// <summary>
        /// Updates an existing user in role.
        /// </summary>
        /// <param name="id">User in role ID.</param>
        /// <param name="userInRole">Updated user in role data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutUserInRole(Guid id, App.DTO.v1.ApiEntities.UserInRole userInRole)
        {
            if (id != userInRole.Id)
            {
                return BadRequest();
            }

            await _bll.UserInRoleService.UpdateAsync(_mapper.Map(userInRole)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new user in role.
        /// </summary>
        /// <param name="userInRole">User in role data.</param>
        /// <returns>The created user in role with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.UserInRole), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.UserInRole>> PostUserInRole(App.DTO.v1.ApiEntities.UserInRole userInRole)
        {
            var data = _mapper.Map(userInRole)!;
            _bll.UserInRoleService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserInRole", new
            {
                id = userInRole.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, userInRole);
        }


        /// <summary>
        /// Deletes a user in role by ID.
        /// </summary>
        /// <param name="id">User in role ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserInRole(Guid id)
        {
            await _bll.UserInRoleService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
