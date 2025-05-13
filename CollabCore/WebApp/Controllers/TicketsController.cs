using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.BLL.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly IAppBLL _bll;

        public TicketsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var res = await _bll.TicketService.AllAsync();
            return View(res);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.TicketService.FindAsync(id.Value, User.GetUserId());
            
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
                FromUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.PersonName)),
                ToUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.PersonName))
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
                _bll.TicketService.Add(vm.Ticket);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.FromUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.PersonName), vm.Ticket.FromUserId);
            vm.ToUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.PersonName), vm.Ticket.ToUserId);

            return View(vm);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _bll.TicketService.FindAsync(id.Value, User.GetUserId());
            if (ticket == null)
            {
                return NotFound();
            }
            
            var vm = new TicketViewModel()
            {
                FromUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()),
                    nameof(Person.PersonName), nameof(Person.Id), ticket.FromUserId),
                ToUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()),
                    nameof(Person.PersonName), nameof(Person.Id), ticket.ToUserId),
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
                _bll.TicketService.Update(vm.Ticket);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.FromUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.PersonName), vm.Ticket.FromUserId);
            vm.ToUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.PersonName), vm.Ticket.ToUserId);
            return View(vm);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _bll.TicketService.FindAsync(id.Value, User.GetUserId());
            
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
            await _bll.TicketService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
