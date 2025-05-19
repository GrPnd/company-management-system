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
    public class TasksApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly TaskApiMapper _mapper = new();

        /// <inheritdoc />
        public TasksApiController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        /// <summary>
        /// Get all tasks.
        /// </summary>
        /// <returns>List of tasks.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Task>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Task>>> GetTasks()
        {
            var data = await _bll.TaskService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a task by ID.
        /// </summary>
        /// <param name="id">Task ID.</param>
        /// <returns>The requested task.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Task), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Task>> GetTask(Guid id)
        {
            var task = await _bll.TaskService.FindAsync(id, User.GetUserId());

            if (task == null)
            {
                return NotFound();
            }

            return _mapper.Map(task)!;
        }

        
        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="id">Task ID.</param>
        /// <param name="task">Updated task data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutTask(Guid id, App.DTO.v1.ApiEntities.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            await _bll.TaskService.UpdateAsync(_mapper.Map(task)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="task">Task data.</param>
        /// <returns>The created task with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Task), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Task>> PostTask(App.DTO.v1.ApiEntities.Task task)
        {
            if (task.Id == Guid.Empty) task.Id = Guid.NewGuid();
            
            var data = _mapper.Map(task)!;
            _bll.TaskService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTask", new
            {
                id = task.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, task);
        }

        /// <summary>
        /// Deletes a task by ID.
        /// </summary>
        /// <param name="id">Task ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _bll.TaskService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
        
        
        /// <summary>
        /// Deletes a task by ID with UserInTeamInTask relation.
        /// </summary>
        /// <param name="taskId">Task ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("task/{taskId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTaskWithRelation(Guid taskId)
        {
            await _bll.TaskService.DeleteTaskWithTeamTaskRelation(taskId);
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
