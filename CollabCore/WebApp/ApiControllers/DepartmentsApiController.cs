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
    public class DepartmentsApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DepartmentApiMapper _mapper = new();

        /// <inheritdoc />
        public DepartmentsApiController(AppBLL bll)
        {
            _bll = bll;
        }

        
        /// <summary>
        /// Get all departments.
        /// </summary>
        /// <returns>List of departments.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Department>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Department>>> GetDepartments()
        {
            var data = await _bll.DepartmentService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a department by ID.
        /// </summary>
        /// <param name="id">Department ID.</param>
        /// <returns>The requested department.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Department), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Department>> GetDepartment(Guid id)
        {
            var department = await _bll.DepartmentService.FindAsync(id, User.GetUserId());
            
            if (department == null)
            {
                return NotFound();
            }

            return _mapper.Map(department)!;
        }

        
        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="id">Department ID.</param>
        /// <param name="department">Updated department data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutDepartment(Guid id, App.DTO.v1.ApiEntities.Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            await _bll.DepartmentService.UpdateAsync(_mapper.Map(department)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="department">Department data.</param>
        /// <returns>The created department with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Department), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Department>> PostDepartment(App.DTO.v1.ApiEntities.Department department)
        {
            var data = _mapper.Map(department)!;
            _bll.DepartmentService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new
            {
                id = department.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, department);
        }

        
        /// <summary>
        /// Deletes a department by ID.
        /// </summary>
        /// <param name="id">Department ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            await _bll.DepartmentService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
