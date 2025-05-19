using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.BLL.DTO;
using App.Domain.Identity;
using Base.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public PersonsController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Persons
        public async Task<IActionResult> Index()
        {
            var res = await _bll.PersonService.AllAsync(User.GetUserId());
            return View(res);
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.PersonService.FindAsync(id.Value, User.GetUserId());
            
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
                Person = new App.BLL.DTO.Person(),
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
                _bll.PersonService.Add(vm.Person, User.GetUserId());
                await _bll.SaveChangesAsync();
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

            var person = await _bll.PersonService.FindAsync(id.Value, User.GetUserId());
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Person entity)
        {
            if (id != entity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.PersonService.Update(entity);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(entity);

        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.PersonService.FindAsync(id.Value, User.GetUserId());
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
            await _bll.PersonService.RemoveAsync(id, User.GetUserId());

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}