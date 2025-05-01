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
    public class StatusesApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly StatusApiMapper _mapper;

        /// <inheritdoc />
        public StatusesApiController(IAppBLL bll, StatusApiMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all statuses.
        /// </summary>
        /// <returns>List of statuses.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Status>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Status>>> GetStatuses()
        {
            var data = await _bll.StatusService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        /// <summary>
        /// Retrieves a status by ID.
        /// </summary>
        /// <param name="id">Status ID.</param>
        /// <returns>The requested status.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Status), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Status>> GetStatus(Guid id)
        {
            var status = await _bll.StatusService.FindAsync(id, User.GetUserId());
            
            if (status == null)
            {
                return NotFound();
            }

            return _mapper.Map(status);
        }

        
        /// <summary>
        /// Updates an existing status.
        /// </summary>
        /// <param name="id">Status ID.</param>
        /// <param name="status">Updated status data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutStatus(Guid id, App.DTO.v1.ApiEntities.Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }

            await _bll.StatusService.UpdateAsync(_mapper.Map(status)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new status.
        /// </summary>
        /// <param name="status">Status data.</param>
        /// <returns>The created status with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Status), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Status>> PostStatus(App.DTO.v1.ApiEntities.Status status)
        {
            var data = _mapper.Map(status)!;
            _bll.StatusService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new
            {
                id = status.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, status);
        }

        
        /// <summary>
        /// Deletes a status by ID.
        /// </summary>
        /// <param name="id">Status ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteStatus(Guid id)
        {
            await _bll.StatusService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
