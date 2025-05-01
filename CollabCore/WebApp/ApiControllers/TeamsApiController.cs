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
    public class TeamsApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly TeamApiMapper _mapper;

        /// <inheritdoc />
        public TeamsApiController(IAppBLL bll, TeamApiMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all teams.
        /// </summary>
        /// <returns>List of teams.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Team>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Team>>> GetTeams()
        {
            var data = await _bll.TeamService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a team by ID.
        /// </summary>
        /// <param name="id">Team ID.</param>
        /// <returns>The requested team.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Team), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Team>> GetTeam(Guid id)
        {
            var team = await _bll.TeamService.FindAsync(id, User.GetUserId());

            if (team == null)
            {
                return NotFound();
            }

            return _mapper.Map(team);
        }

        
        /// <summary>
        /// Updates an existing team.
        /// </summary>
        /// <param name="id">Team ID.</param>
        /// <param name="team">Updated team data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutTeam(Guid id, App.DTO.v1.ApiEntities.Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            await _bll.TeamService.UpdateAsync(_mapper.Map(team)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new team.
        /// </summary>
        /// <param name="team">Team data.</param>
        /// <returns>The created team with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Team), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Team>> PostTeam(App.DTO.v1.ApiEntities.Team team)
        {
            var data = _mapper.Map(team)!;
            _bll.TeamService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new
            {
                id = team.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, team);
        }

        /// <summary>
        /// Deletes a team by ID.
        /// </summary>
        /// <param name="id">Team ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            await _bll.TeamService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
