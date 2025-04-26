using App.BLL.Contracts;
using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.BLL.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class MeetingsController : Controller
    {
        private readonly IAppBLL _bll;

        public MeetingsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            var res = await _bll.MeetingService.AllAsync();
            return View(res);
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.MeetingService.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Meetings/Create
        public async Task<IActionResult> Create()
        {
            var vm = new MeetingViewModel()
            {
                TeamSelectList = new SelectList(await _bll.TeamService.AllAsync(User.GetUserId()), nameof(Team.Id),
                    nameof(Team.Name))
            };
            return View(vm);
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetingViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.MeetingService.Add(vm.Meeting);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.TeamSelectList = new SelectList(await _bll.TeamService.AllAsync(User.GetUserId()), nameof(Team.Id),
                nameof(Team.Name), vm.Meeting.TeamId);
            return View(vm);
        }

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var meeting = await _bll.MeetingService.FindAsync(id.Value, User.GetUserId());
            if (meeting == null)
            {
                return NotFound();
            }

            var vm = new MeetingViewModel()
            {
                TeamSelectList = new SelectList(await _bll.TeamService.AllAsync(User.GetUserId()),
                    nameof(Team.Id), nameof(Team.Name), meeting.TeamId),
                Meeting = meeting
            };
            return View(vm);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MeetingViewModel vm)
        {
            if (id != vm.Meeting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.MeetingService.Update(vm.Meeting);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.TeamSelectList = new SelectList(await _bll.TeamService.AllAsync(User.GetUserId()),
                nameof(Team.Id), nameof(Team.Name), vm.Meeting.TeamId);
            return View(vm);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var meeting = await _bll.MeetingService.FindAsync(id.Value, User.GetUserId());

            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.MeetingService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
