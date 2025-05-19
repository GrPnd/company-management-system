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
    public class TicketsApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly TicketApiMapper _mapper = new();

        /// <inheritdoc />
        public TicketsApiController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        /// <summary>
        /// Get all tickets.
        /// </summary>
        /// <returns>List of tickets.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Ticket>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Ticket>>> GetTickets()
        {
            var data = await _bll.TicketService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a ticket by ID.
        /// </summary>
        /// <param name="id">Ticket ID.</param>
        /// <returns>The requested ticket.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Ticket), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Ticket>> GetTicket(Guid id)
        {
            var ticket = await _bll.TicketService.FindAsync(id, User.GetUserId());

            if (ticket == null)
            {
                return NotFound();
            }

            return _mapper.Map(ticket)!;
        }
        
            
        /// <summary>
        /// Get all tickets that are sent to a user in a team.
        /// </summary>
        /// <param name="userId">Sent to user ID.</param>
        /// <param name="teamId">Team ID.</param>
        /// <returns>List of tickets.</returns>
        [HttpGet("user/{userId}/team/{teamId}")]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Ticket>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Ticket>>> GetTicketsSentToUserInTeam(Guid userId, Guid teamId)
        {
            var data = await _bll.TicketService.GetTicketsSentToUserInTeam(userId, teamId);
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }
        
        
        /// <summary>
        /// Get all tickets that are sent by the user.
        /// </summary>
        /// <param name="userId">Sent by user ID.</param>
        /// <returns>List of tickets by user.</returns>
        [HttpGet("by/{userId}")]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Ticket>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Ticket>>> GetTicketsSentByPersonId(Guid userId)
        {
            var data = await _bll.TicketService.GetTicketsSentByPersonId(userId);
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }
        
        /// <summary>
        /// Get all tickets that are sent to the user.
        /// </summary>
        /// <param name="userId">Sent to user ID.</param>
        /// <returns>List of tickets to user.</returns>
        [HttpGet("to/{userId}")]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Ticket>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Ticket>>> GetTicketsSentToPersonId(Guid userId)
        {
            var data = await _bll.TicketService.GetTicketsSentToPersonId(userId);
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Updates an existing ticket.
        /// </summary>
        /// <param name="id">Ticket ID.</param>
        /// <param name="ticket">Updated ticket data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutTicket(Guid id, App.DTO.v1.ApiEntities.Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            await _bll.TicketService.UpdateAsync(_mapper.Map(ticket)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new ticket.
        /// </summary>
        /// <param name="ticket">Ticket data.</param>
        /// <returns>The created ticket with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Ticket), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Ticket>> PostTicket(App.DTO.v1.ApiEntities.Ticket ticket)
        {
            var data = _mapper.Map(ticket)!;
            _bll.TicketService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new
            {
                id = ticket.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, ticket);
        }


        /// <summary>
        /// Deletes a ticket by ID.
        /// </summary>
        /// <param name="id">Ticket ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            await _bll.TicketService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
