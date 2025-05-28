using App.DAL.Contracts;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Base.Helpers;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UsersInTeamsController : Controller
    {
        private readonly IAppUOW _uow;
        private readonly UserManager<AppUser> _userManager;

        public UsersInTeamsController(IAppUOW uow, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        // GET: UserInTeams
        public async Task<IActionResult> Index()
        {
            var res = await _uow.UserInTeamRepository.AllAsync();
            return View(res);
        }

        // GET: UserInTeams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.UserInTeamRepository.FindAsync(id.Value);

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: UserInTeams/Create
        public async Task<IActionResult> Create()
        {
            var users = await _userManager.Users
                .Select(u => new
                {
                    u.Id,
                    FullName = u.FirstName + " " + u.LastName
                })
                .ToListAsync();

            var teams = await _uow.TeamRepository.AllAsync();

            var vm = new UserInTeamViewModel()
            {
                UserSelectList = new SelectList(users, "Id", "FullName"),
                TeamSelectList = new SelectList(teams, nameof(Team.Id), nameof(Team.TeamName))
            };

            return View(vm);
        }

        // POST: UserInTeams/Create
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
            

            var users = await _userManager.Users
                .Select(u => new
                {
                    u.Id,
                    FullName = u.FirstName + " " + u.LastName
                })
                .ToListAsync();

            var teams = await _uow.TeamRepository.AllAsync();

            vm.UserSelectList = new SelectList(users, "Id", "FullName", vm.UserInTeam.UserId);
            vm.TeamSelectList = new SelectList(teams, nameof(Team.Id), nameof(Team.TeamName), vm.UserInTeam.TeamId);

            return View(vm);
        }

        // GET: UserInTeams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeam = await _uow.UserInTeamRepository.FindAsync(id.Value);
            if (userInTeam == null)
            {
                return NotFound();
            }

            var users = await _userManager.Users
                .Select(u => new
                {
                    u.Id,
                    FullName = u.FirstName + " " + u.LastName
                })
                .ToListAsync();

            var teams = await _uow.TeamRepository.AllAsync();

            var vm = new UserInTeamViewModel()
            {
                UserSelectList = new SelectList(users, "Id", "FullName"),
                TeamSelectList = new SelectList(teams, nameof(Team.Id), nameof(Team.TeamName)),
                UserInTeam = userInTeam
            };

            return View(vm);
        }

        // POST: UserInTeams/Edit/5
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

            var users = await _userManager.Users
                .Select(u => new
                {
                    u.Id,
                    FullName = u.FirstName + " " + u.LastName
                })
                .ToListAsync();

            var teams = await _uow.TeamRepository.AllAsync();

            vm.UserSelectList = new SelectList(users, "Id", "FullName", vm.UserInTeam.UserId);
            vm.TeamSelectList = new SelectList(teams, nameof(Team.Id), nameof(Team.TeamName), vm.UserInTeam.TeamId);

            return View(vm);
        }

        // GET: UserInTeams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeam = await _uow.UserInTeamRepository.FindAsync(id.Value);

            if (userInTeam == null)
            {
                return NotFound();
            }

            return View(userInTeam);
        }

        // POST: UserInTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.UserInTeamRepository.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}