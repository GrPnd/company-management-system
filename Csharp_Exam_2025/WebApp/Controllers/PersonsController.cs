using Microsoft.AspNetCore.Mvc;
using App.DAL.Contracts;
using App.Domain.Identity;
using Base.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IAppUOW _uow;
        private readonly UserManager<AppUser> _userManager;

        public PersonsController(IAppUOW uow, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        // GET: Persons
        public async Task<IActionResult> Index()
        {
            var res = await _uow.PersonRepository.AllAsync(User.GetUserId());
            return View(res);
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.PersonRepository.FindAsync(id.Value, User.GetUserId());

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Persons/Create
        public async Task<IActionResult> Create()
        {
            var users = await _userManager.Users
                .Include(u => u.Persons) // ensure Persons are loaded
                .ToListAsync();

            var vm = new PersonViewModel()
            {
                Person = new App.DAL.DTO.Person(),
                UsersSelectList = new SelectList(users, "Id", "UserName")
            };
            return View(vm);
        }

        // POST: Persons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.PersonRepository.Add(vm.Person, User.GetUserId());
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate SelectList if model state is invalid
            var users = await _userManager.Users.ToListAsync();
            vm.UsersSelectList = new SelectList(users, "Id", "UserName");
            return View(vm);
        }

        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _uow.PersonRepository.FindAsync(id.Value, User.GetUserId());
            if (person == null)
            {
                return NotFound();
            }

            var users = await _userManager.Users.ToListAsync();
            var vm = new PersonViewModel()
            {
                UsersSelectList = new SelectList(users, "Id", "UserName"),
                Person = person
            };
            return View(vm);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PersonViewModel vm)
        {
            if (id != vm.Person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PersonRepository.Update(vm.Person);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var users = await _userManager.Users.ToListAsync();
            vm.UsersSelectList = new SelectList(users, "Id", "UserName", vm.Person.Id);
            return View(vm);
        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.PersonRepository.FindAsync(id.Value, User.GetUserId());
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PersonRepository.RemoveAsync(id, User.GetUserId());

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}