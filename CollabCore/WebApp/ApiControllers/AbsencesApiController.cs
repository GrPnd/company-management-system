using App.BLL;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Domain;
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
    public class AbsencesApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AbsenceApiMapper _mapper = new();

        /// <inheritdoc />
        public AbsencesApiController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        /// <summary>
        /// Get all absences.
        /// </summary>
        /// <returns>List of absences.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Absence>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Absence>>> GetAbsences()
        {
            var data = await _bll.AbsenceService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves an absence by ID.
        /// </summary>
        /// <param name="id">Absence ID.</param>
        /// <returns>The requested absence.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Absence), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Absence>> GetAbsence(Guid id)
        {
            var absence = await _bll.AbsenceService.FindAsync(id, User.GetUserId());

            if (absence == null)
            {
                return NotFound();
            }

            return _mapper.Map(absence)!;
        }
        
        
        /// <summary>
        /// Get all absences that are sent to a user in a team.
        /// </summary>
        /// <param name="userId">Sent to user ID.</param>
        /// <param name="teamId">Team ID.</param>
        /// <returns>List of absences.</returns>
        [HttpGet("user/{userId}/team/{teamId}")]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Absence>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Absence>>> GetAbsencesSentToUserInTeam(Guid userId, Guid teamId)
        {
            var data = await _bll.AbsenceService.GetAbsencesSentToUserInTeam(userId, teamId);
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }
        
        
        /// <summary>
        /// Get all absences that are sent by the user.
        /// </summary>
        /// <param name="userId">Sent by user ID.</param>
        /// <returns>List of absences by user.</returns>
        [HttpGet("by/user/{userId}")]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Absence>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Absence>>> GetAbsencesSentByPersonId(Guid userId)
        {
            var data = await _bll.AbsenceService.GetAbsencesSentByPersonId(userId);
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }
        
        
        /// <summary>
        /// Get all absences that are sent to the user.
        /// </summary>
        /// <param name="userId">Sent to user ID.</param>
        /// <returns>List of absences to user.</returns>
        [HttpGet("to/user/{userId}")]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Absence>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Absence>>> GetAbsencesSentToPersonId(Guid userId)
        {
            var data = await _bll.AbsenceService.GetAbsencesSentToPersonId(userId);
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Updates an existing absence.
        /// </summary>
        /// <param name="id">Absence ID.</param>
        /// <param name="absence">Updated absence data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAbsence(Guid id, App.DTO.v1.ApiEntities.Absence absence)
        {
            if (id != absence.Id)
            {
                return BadRequest();
            }

            await _bll.AbsenceService.UpdateAsync(_mapper.Map(absence)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
        
        /// <summary>
        /// Creates a new absence.
        /// </summary>
        /// <param name="absence">Absence data.</param>
        /// <returns>The created absence with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Absence), StatusCodes.Status200OK)]
        public async Task<ActionResult<Absence>> PostAbsence(App.DTO.v1.ApiEntities.Absence absence)
        {
            if (absence.StartDate.Kind == DateTimeKind.Unspecified)
            {
                absence.StartDate = DateTime.SpecifyKind(absence.StartDate, DateTimeKind.Utc);
            }
            else
            {
                absence.StartDate = absence.StartDate.ToUniversalTime();
            }
            
            if (absence.EndDate.Kind == DateTimeKind.Unspecified)
            {
                absence.EndDate = DateTime.SpecifyKind(absence.EndDate, DateTimeKind.Utc);
            }
            else
            {
                absence.EndDate = absence.EndDate.ToUniversalTime();
            }
            
            var data = _mapper.Map(absence)!;
            _bll.AbsenceService.Add(data);
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction("GetAbsence", new
            {
                id = absence.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, absence);
        }

        
        /// <summary>
        /// Deletes an absence by ID.
        /// </summary>
        /// <param name="id">Absence ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAbsence(Guid id)
        {
            await _bll.AbsenceService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
