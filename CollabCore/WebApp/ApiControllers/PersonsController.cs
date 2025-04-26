using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.BLL.DTO;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PersonsController(AppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all persons for currently logged in user
        /// </summary>
        /// <returns>List of persons</returns>
        // GET: api/Persons
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ApiEntities.Person> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.Person>>> GetPersons()
        {
            var data = (await _bll.PersonService.AllAsync(User.GetUserId())).ToList();
            // TODO - add mapper
            var res = data.Select(p => new App.DTO.v1.ApiEntities.Person()
            {
                Id = p.Id,
                PersonName = p.PersonName

            }).ToList();
            return res;
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var person = await _bll.PersonService.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/Persons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _bll.PersonService.Update(person);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Persons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _bll.PersonService.Add(person, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new
            {
                // todo - get person id
                id = person.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, person);
        }


        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var person = await _bll.PersonService.FindAsync(id, User.GetUserId());
            if (person == null)
            {
                return NotFound();
            }

            _bll.PersonService.Remove(person);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
