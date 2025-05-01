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
    public class WorkDaysApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly WorkDayApiMapper _mapper;

        /// <inheritdoc />
        public WorkDaysApiController(IAppBLL bll, WorkDayApiMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        
        /// <summary>
        /// Get all workdays.
        /// </summary>
        /// <returns>List of workdays.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.WorkDay>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.WorkDay>>> GetWorkDays()
        {
            var data = await _bll.WorkDayService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a workday by ID.
        /// </summary>
        /// <param name="id">Workday ID.</param>
        /// <returns>The requested workday.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.WorkDay), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.WorkDay>> GetWorkDay(Guid id)
        {
            var workDay = await _bll.WorkDayService.FindAsync(id, User.GetUserId());

            if (workDay == null)
            {
                return NotFound();
            }

            return _mapper.Map(workDay);
        }

        
        /// <summary>
        /// Updates an existing workday.
        /// </summary>
        /// <param name="id">Workday ID.</param>
        /// <param name="workDay">Updated workday data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutWorkDay(Guid id, App.DTO.v1.ApiEntities.WorkDay workDay)
        {
            if (id != workDay.Id)
            {
                return BadRequest();
            }

            await _bll.WorkDayService.UpdateAsync(_mapper.Map(workDay)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new workday.
        /// </summary>
        /// <param name="workDay">Workday data.</param>
        /// <returns>The created workday with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.WorkDay), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.WorkDay>> PostWorkDay(App.DTO.v1.ApiEntities.WorkDay workDay)
        {
            var data = _mapper.Map(workDay)!;
            _bll.WorkDayService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWorkDay", new
            {
                id = workDay.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, workDay);
        }


        /// <summary>
        /// Deletes a workday by ID.
        /// </summary>
        /// <param name="id">Workday ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteWorkDay(Guid id)
        {
            await _bll.WorkDayService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
