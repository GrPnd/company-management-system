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
    public class UsersInTeamsInTasksApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserInTeamInTaskApiMapper _mapper;

        /// <inheritdoc />
        public UsersInTeamsInTasksApiController(IAppBLL bll, UserInTeamInTaskApiMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        
        /// <summary>
        /// Get all users in teams in tasks.
        /// </summary>
        /// <returns>List of users in teams in tasks.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.UserInTeamInTask>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.UserInTeamInTask>>> GetUsersInTeamsInTasks()
        {
            var data = await _bll.UserInTeamInTaskService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a user in team in task by ID.
        /// </summary>
        /// <param name="id">User in team in task ID.</param>
        /// <returns>The requested user in team in task.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.UserInTeamInTask), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.UserInTeamInTask>> GetUserInTeamInTask(Guid id)
        {
            var userInTeamInTask = await _bll.UserInTeamInTaskService.FindAsync(id, User.GetUserId());

            if (userInTeamInTask == null)
            {
                return NotFound();
            }

            return _mapper.Map(userInTeamInTask);
        }

        
        /// <summary>
        /// Updates an existing user in team in task.
        /// </summary>
        /// <param name="id">user in team in task ID.</param>
        /// <param name="userInTeamInTask">Updated user in team in task data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutUserInTeamInTask(Guid id, App.DTO.v1.ApiEntities.UserInTeamInTask userInTeamInTask)
        {
            if (id != userInTeamInTask.Id)
            {
                return BadRequest();
            }

            await _bll.UserInTeamInTaskService.UpdateAsync(_mapper.Map(userInTeamInTask)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new user in team in task.
        /// </summary>
        /// <param name="userInTeamInTask">User in team in task data.</param>
        /// <returns>The created user in team in task with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.UserInTeamInTask), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.UserInTeamInTask>> PostUserInTeamInTask(App.DTO.v1.ApiEntities.UserInTeamInTask userInTeamInTask)
        {
            var data = _mapper.Map(userInTeamInTask)!;
            _bll.UserInTeamInTaskService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserInTeamInTask", new
            {
                id = userInTeamInTask.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, userInTeamInTask);
        }


        /// <summary>
        /// Deletes a user in team in task by ID.
        /// </summary>
        /// <param name="id">User in team in task ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserInTeamInTask(Guid id)
        {
            await _bll.UserInTeamInTaskService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }   
    }
}
