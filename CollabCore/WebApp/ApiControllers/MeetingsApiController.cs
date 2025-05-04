using App.BLL;
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
    public class MeetingsApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly MeetingApiMapper _mapper = new();

        /// <inheritdoc />
        public MeetingsApiController(AppBLL bll)
        {
            _bll = bll;
        }

        
        /// <summary>
        /// Get all meetings.
        /// </summary>
        /// <returns>List of meetings.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Meeting>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Meeting>>> GetMeetings()
        {
            var data = await _bll.MeetingService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        /// <summary>
        /// Retrieves a meeting by ID.
        /// </summary>
        /// <param name="id">Meeting ID.</param>
        /// <returns>The requested meeting.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Meeting), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Meeting>> GetMeeting(Guid id)
        {
            var meeting = await _bll.MeetingService.FindAsync(id, User.GetUserId());

            if (meeting == null)
            {
                return NotFound();
            }

            return _mapper.Map(meeting)!;
        }

        
        /// <summary>
        /// Updates an existing meeting.
        /// </summary>
        /// <param name="id">Meeting ID.</param>
        /// <param name="meeting">Updated meeting data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutMeeting(Guid id, App.DTO.v1.ApiEntities.Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return BadRequest();
            }
            await _bll.MeetingService.UpdateAsync(_mapper.Map(meeting)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Creates a new meeting.
        /// </summary>
        /// <param name="meeting">Meeting data.</param>
        /// <returns>The created meeting with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Meeting), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Meeting>> PostMeeting(App.DTO.v1.ApiEntities.Meeting meeting)
        {
            var data = _mapper.Map(meeting)!;
            _bll.MeetingService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetMeeting", new
            {
                id = meeting.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, meeting);
        }

        
        /// <summary>
        /// Deletes a meeting by ID.
        /// </summary>
        /// <param name="id">Meeting ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMeeting(Guid id)
        {
            await _bll.MeetingService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
