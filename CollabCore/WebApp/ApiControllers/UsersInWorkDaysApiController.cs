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
    public class UsersInWorkDaysApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserInWorkDayApiMapper _mapper = new();

        /// <inheritdoc />
        public UsersInWorkDaysApiController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        
        /// <summary>
        /// Get all users in workdays.
        /// </summary>
        /// <returns>List of users in workdays.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.UserInWorkDay>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.UserInWorkDay>>> GetUsersInWorkDays()
        {
            var data = await _bll.UserInWorkDayService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a user in workday by ID.
        /// </summary>
        /// <param name="id">User in workday ID.</param>
        /// <returns>The requested user in workday.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.UserInWorkDay), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.UserInWorkDay>> GetUserInWorkDay(Guid id)
        {
            var userInWorkDay = await _bll.UserInWorkDayService.FindAsync(id, User.GetUserId());

            if (userInWorkDay == null)
            {
                return NotFound();
            }

            return _mapper.Map(userInWorkDay)!;
        }

        
        /// <summary>
        /// Updates an existing user in workday.
        /// </summary>
        /// <param name="id">User in workday ID.</param>
        /// <param name="userInWorkDay">Updated user in workday data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutUserInWorkDay(Guid id, App.DTO.v1.ApiEntities.UserInWorkDay userInWorkDay)
        {
            if (id != userInWorkDay.Id)
            {
                return BadRequest();
            }

            await _bll.UserInWorkDayService.UpdateAsync(_mapper.Map(userInWorkDay)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new user in workday.
        /// </summary>
        /// <param name="userInWorkDay">User in workday data.</param>
        /// <returns>The created user in workday with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.UserInWorkDay), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.UserInWorkDay>> PostUserInWorkDay(App.DTO.v1.ApiEntities.UserInWorkDay userInWorkDay)
        {
            var data = _mapper.Map(userInWorkDay)!;
            _bll.UserInWorkDayService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserInWorkDay", new
            {
                id = userInWorkDay.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, userInWorkDay);
        }

        
        /// <summary>
        /// Deletes a user in workday by ID.
        /// </summary>
        /// <param name="id">User in workday ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserInWorkDay(Guid id)
        {
            await _bll.UserInWorkDayService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
