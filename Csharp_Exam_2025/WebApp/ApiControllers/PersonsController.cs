using App.DAL.Contracts;
using App.DTO.v1.ApiMappers;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <inheritdoc />
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PersonsController : ControllerBase
{
    private readonly IAppUOW _uow;
    private readonly PersonApiMapper _mapper = new();

    /// <inheritdoc />
    public PersonsController(IAppUOW uow)
    {
        _uow = uow;
    }


    /// <summary>
    /// Get all persons.
    /// </summary>
    /// <returns>List of persons.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.ApiEntities.Person>), StatusCodes.Status200OK)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Person>>> GetPersons()
    {
        var data = await _uow.PersonRepository.AllAsync();
        var res = data.Select(p => _mapper.MapTo(p)!).ToList();
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
        var person = await _uow.PersonRepository.FindAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return _mapper.MapTo(person)!;
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

        await _uow.PersonRepository.UpdateAsync(_mapper.MapFrom(person)!);
        await _uow.SaveChangesAsync();

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
        var data = _mapper.MapFrom(person)!;
        _uow.PersonRepository.Add(data);
        await _uow.SaveChangesAsync();

        return CreatedAtAction("GetPerson", new
        {
            id = person.Id,
            version = HttpContext.GetRequestedApiVersion()!.ToString()
        }, person);
    }


    /// <summary>
    /// Creates a new person to a user.
    /// </summary>
    /// <param name="person">Person data.</param>
    /// <param name="userId">App user ID</param>
    /// <returns>The created person with a location header.</returns>
    [HttpPost("user/{userId}")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(App.DTO.v1.ApiEntities.Person), StatusCodes.Status200OK)]
    public async Task<ActionResult<App.DTO.v1.ApiEntities.Person>> PostPersonToUser(
        App.DTO.v1.ApiEntities.Person person, Guid userId)
    {
        var data = _mapper.MapFrom(person)!;
        _uow.PersonRepository.Add(data, userId);
        await _uow.SaveChangesAsync();

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
        await _uow.PersonRepository.RemoveAsync(id);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}