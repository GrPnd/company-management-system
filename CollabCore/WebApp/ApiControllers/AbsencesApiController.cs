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
        public AbsencesApiController(AppBLL bll)
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
