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
    public class MessagesApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly MessageApiMapper _mapper = new();

        /// <inheritdoc />
        public MessagesApiController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        /// <summary>
        /// Get all messages.
        /// </summary>
        /// <returns>List of messages.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Message>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Message>>> GetMessages()
        {
            var data = await _bll.MessageService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a message by ID.
        /// </summary>
        /// <param name="id">Message ID.</param>
        /// <returns>The requested message.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Message), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Message>> GetMessage(Guid id)
        {
            var message = await _bll.MessageService.FindAsync(id, User.GetUserId());

            if (message == null)
            {
                return NotFound();
            }

            return _mapper.Map(message)!;
        }

        
        /// <summary>
        /// Updates an existing message.
        /// </summary>
        /// <param name="id">Message ID.</param>
        /// <param name="message">Updated message data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutMessage(Guid id, App.DTO.v1.ApiEntities.Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            await _bll.MessageService.UpdateAsync(_mapper.Map(message)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new message.
        /// </summary>
        /// <param name="message">Message data.</param>
        /// <returns>The created message with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Message), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Message>> PostMessage(App.DTO.v1.ApiEntities.Message message)
        {
            var data = _mapper.Map(message)!;
            _bll.MessageService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new
            {
                id = message.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, message);
        }

        
        /// <summary>
        /// Deletes a message by ID.
        /// </summary>
        /// <param name="id">Message ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMessage(Guid id)
        {
            await _bll.MessageService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
