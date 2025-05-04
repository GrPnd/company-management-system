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
    public class PersonsApiController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PersonApiMapper _mapper = new();

        /// <inheritdoc />
        public PersonsApiController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        /// <summary>
        /// Get all persons.
        /// </summary>
        /// <returns>List of persons.</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Person>), StatusCodes.Status200OK )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Person>>> GetPersons()
        {
            var data = await _bll.PersonService.AllAsync(User.GetUserId());
            var res = data.Select(p => _mapper.Map(p)!).ToList();
            return res;
        }

        
        /// <summary>
        /// Retrieves a person by ID.
        /// </summary>
        /// <param name="id">Person ID.</param>
        /// <returns>The requested person.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Person>> GetPerson(Guid id)
        {
            var person = await _bll.PersonService.FindAsync(id, User.GetUserId());

            if (person == null)
            {
                return NotFound();
            }

            return _mapper.Map(person)!;
        }

        
        /// <summary>
        /// Updates an existing person.
        /// </summary>
        /// <param name="id">Person ID.</param>
        /// <param name="person">Updated person data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutPerson(Guid id, App.DTO.v1.ApiEntities.Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            await _bll.PersonService.UpdateAsync(_mapper.Map(person)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="person">Person data.</param>
        /// <returns>The created person with a location header.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Person), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.ApiEntities.Person>> PostPerson(App.DTO.v1.ApiEntities.Person person)
        {
            var data = _mapper.Map(person)!;
            _bll.PersonService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new
            {
                id = person.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, person);
        }


        /// <summary>
        /// Deletes a person by ID.
        /// </summary>
        /// <param name="id">Person ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            await _bll.PersonService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
