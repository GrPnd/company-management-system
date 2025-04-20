using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.DAL.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class UsersInTeamsController : Controller
    {
        private readonly IAppUOW _uow;

        public UsersInTeamsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: UsersInTeams
        public async Task<IActionResult> Index()
        {
            var res = await _uow.UserInTeamRepository.AllAsync(User.GetUserId());
            return View(res);
        }

        // GET: UsersInTeams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.UserInTeamRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: UsersInTeams/Create
        public async Task<IActionResult> Create()
        {
            var vm = new UserInTeamViewModel()
            {
                TeamSelectList = new SelectList(await _uow.TeamRepository.AllAsync(User.GetUserId()), nameof(Team.Id),
                    nameof(Team.Name)),
                UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.Id))
            };
            return View(vm);
        }

        // POST: UsersInTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserInTeamViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.UserInTeamRepository.Add(vm.UserInTeam);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.TeamSelectList = new SelectList(await _uow.TeamRepository.AllAsync(User.GetUserId()), nameof(Team.Id),
                nameof(Team.Name), vm.UserInTeam.TeamId);
            vm.UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id), 
                nameof(Person.Id), vm.UserInTeam.UserId);
            
            return View(vm);
        }

        // GET: UsersInTeams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeam = await _uow.UserInTeamRepository.FindAsync(id.Value, User.GetUserId());
            if (userInTeam == null)
            {
                return NotFound();
            }
            
            var vm = new UserInTeamViewModel()
            {
                TeamSelectList = new SelectList(await _uow.TeamRepository.AllAsync(User.GetUserId()), nameof(Team.Id),
                    nameof(Team.Name), userInTeam.TeamId),
                UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.Id), userInTeam.UserId),
                UserInTeam = userInTeam
            };
            return View(vm);
        }

        // POST: UsersInTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserInTeamViewModel vm)
        {
            if (id != vm.UserInTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.UserInTeamRepository.Update(vm.UserInTeam);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.TeamSelectList = new SelectList(await _uow.TeamRepository.AllAsync(User.GetUserId()), nameof(Team.Id),
                nameof(Team.Name), vm.UserInTeam.TeamId);
            vm.UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id), 
                nameof(Person.Id), vm.UserInTeam.UserId);
            
            return View(vm);
        }

        // GET: UsersInTeams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeam = await _uow.UserInTeamRepository.FindAsync(id.Value, User.GetUserId());
            
            if (userInTeam == null)
            {
                return NotFound();
            }

            return View(userInTeam);
        }

        // POST: UsersInTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.UserInTeamRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
