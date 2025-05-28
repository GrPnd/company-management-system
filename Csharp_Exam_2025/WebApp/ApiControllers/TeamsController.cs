using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TeamsController : ControllerBase
    {
        private readonly IAppUOW _uow;
        private readonly App.DTO.v1.ApiMappers.TeamApiMapper _mapper = new();

        public TeamsController(IAppUOW uow)
        {
            _uow = uow;
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
            var data = await _uow.TeamRepository.AllAsync(User.GetUserId());
            var res = data.Select(t => _mapper.MapTo(t)!).ToList();
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
            var team = await _uow.TeamRepository.FindAsync(id, User.GetUserId());

            if (team == null)
            {
                return NotFound();
            }

            return _mapper.MapTo(team)!;
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

            await _uow.TeamRepository.UpdateAsync(_mapper.MapFrom(team)!, User.GetUserId());
            await _uow.SaveChangesAsync();

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
            var entity = _mapper.MapFrom(team)!;
            _uow.TeamRepository.Add(entity, User.GetUserId());
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new
            {
                id = entity.Id,
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
            await _uow.TeamRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return NoContent();
        }
    }
}

