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
    public class SchedulesApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ScheduleApiMapper _mapper = new();

        /// <inheritdoc />
        public SchedulesApiController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        /// <summary>
        /// Get all schedules.
        /// </summary>
        /// <returns>List of schedules.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Schedule>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Schedule>>> GetSchedules()
        {
            var data = await _bll.ScheduleService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a schedule by ID.
        /// </summary>
        /// <param name="id">Schedule ID.</param>
        /// <returns>The requested schedule.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Schedule), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Schedule>> GetSchedule(Guid id)
        {
            var schedule = await _bll.ScheduleService.FindAsync(id, User.GetUserId());
            
            if (schedule == null)
            {
                return NotFound();
            }

            return _mapper.Map(schedule)!;
        }

        
        /// <summary>
        /// Updates an existing schedule.
        /// </summary>
        /// <param name="id">Schedule ID.</param>
        /// <param name="schedule">Updated schedule data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutSchedule(Guid id, App.DTO.v1.ApiEntities.Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest();
            }

            await _bll.ScheduleService.UpdateAsync(_mapper.Map(schedule)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new schedule.
        /// </summary>
        /// <param name="schedule">Schedule data.</param>
        /// <returns>The created schedule with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Schedule), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Schedule>> PostSchedule(App.DTO.v1.ApiEntities.Schedule schedule)
        {
            var data = _mapper.Map(schedule)!;
            _bll.ScheduleService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSchedule", new
            {
                id = schedule.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, schedule);
        }

        
        /// <summary>
        /// Deletes a schedule by ID.
        /// </summary>
        /// <param name="id">Schedule ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSchedule(Guid id)
        {
            await _bll.ScheduleService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
