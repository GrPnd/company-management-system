using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.DAL.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class StatusesController : Controller
    {
        private readonly IAppUOW _uow;

        public StatusesController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Statuses
        public async Task<IActionResult> Index()
        {
            var res = await _uow.StatusRepository.AllAsync();
            return View(res);
        }

        // GET: Statuses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.StatusRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Statuses/Create
        public IActionResult Create()
        {
            var vm = new StatusViewModel();
            return View(vm);
        }

        // POST: Statuses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StatusViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = new Status()
                {
                    Name = vm.Name,
                };
                
                _uow.StatusRepository.Add(entity);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Statuses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _uow.StatusRepository.FindAsync(id.Value, User.GetUserId());
            if (status == null)
            {
                return NotFound();
            }
            
            var vm = new StatusViewModel()
            {
                Id = status.Id,
                Name = status.Name
            };
            
            return View(vm);
        }

        // POST: Statuses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StatusViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var entity = await _uow.StatusRepository.FindAsync(vm.Id, User.GetUserId());
                if (entity == null)
                {
                    return NotFound();
                }
                
                entity.Name = vm.Name;
                
                _uow.StatusRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: Statuses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _uow.StatusRepository.FindAsync(id.Value, User.GetUserId());
            
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Statuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.StatusRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
