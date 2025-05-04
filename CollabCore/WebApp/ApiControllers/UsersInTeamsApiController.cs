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
    public class UsersInTeamsApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserInTeamApiMapper _mapper = new();

        /// <inheritdoc />
        public UsersInTeamsApiController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        /// <summary>
        /// Get all users in teams.
        /// </summary>
        /// <returns>List of users in teams.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.UserInTeam>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.UserInTeam>>> GetUsersInTeams()
        {
            var data = await _bll.UserInTeamService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a user in team by ID.
        /// </summary>
        /// <param name="id">User in team ID.</param>
        /// <returns>The requested user in team.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.UserInTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.UserInTeam>> GetUserInTeam(Guid id)
        {
            var userInTeam = await _bll.UserInTeamService.FindAsync(id, User.GetUserId());

            if (userInTeam == null)
            {
                return NotFound();
            }

            return _mapper.Map(userInTeam)!;
        }

        
        /// <summary>
        /// Updates an existing user in team.
        /// </summary>
        /// <param name="id">User in team ID.</param>
        /// <param name="userInTeam">Updated user in team data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutUserInTeam(Guid id, App.DTO.v1.ApiEntities.UserInTeam userInTeam)
        {
            if (id != userInTeam.Id)
            {
                return BadRequest();
            }

            await _bll.UserInTeamService.UpdateAsync(_mapper.Map(userInTeam)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new user in team.
        /// </summary>
        /// <param name="userInTeam">User in team data.</param>
        /// <returns>The created user in team with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.UserInTeam), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.UserInTeam>> PostUserInTeam(App.DTO.v1.ApiEntities.UserInTeam userInTeam)
        {
            var data = _mapper.Map(userInTeam)!;
            _bll.UserInTeamService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserInTeam", new
            {
                id = userInTeam.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, userInTeam);
        }

        
        /// <summary>
        /// Deletes a user in team by ID.
        /// </summary>
        /// <param name="id">User in team ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserInTeam(Guid id)
        {
            await _bll.UserInTeamService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
