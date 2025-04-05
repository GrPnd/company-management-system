using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Domain;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly IAppUOW _uow;

        public TicketsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var res = await _uow.TicketRepository.AllAsync();
            return View(res);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.TicketRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Tickets/Create
        public async Task<IActionResult> Create()
        {
            var vm = new TicketViewModel()
            {
                FromUserSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.Id)),
                ToUserSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.Id))
            };
            return View(vm);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.TicketRepository.Add(vm.Ticket);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.FromUserSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.Id), vm.Ticket.FromUserId);
            vm.ToUserSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.Id), vm.Ticket.ToUserId);

            return View(vm);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _uow.TicketRepository.FindAsync(id.Value, User.GetUserId());
            if (ticket == null)
            {
                return NotFound();
            }
            
            var vm = new TicketViewModel()
            {
                FromUserSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()),
                    nameof(Person.Id), nameof(Person.Id), ticket.FromUserId),
                ToUserSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()),
                    nameof(Person.Id), nameof(Person.Id), ticket.ToUserId),
                Ticket = ticket
            };
            return View(vm);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TicketViewModel vm)
        {
            if (id != vm.Ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.TicketRepository.Update(vm.Ticket);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.FromUserSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.Id), vm.Ticket.FromUserId);
            vm.ToUserSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.Id), vm.Ticket.ToUserId);
            return View(vm);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _uow.TicketRepository.FindAsync(id.Value, User.GetUserId());
            
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.TicketRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
