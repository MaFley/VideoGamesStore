using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGamesStore.Models;
using VideoGamesStore.Models.Data;
using VideoGamesStore.ViewModels.Orders;

namespace VideoGamesStore.Controllers
{
    // [Authorize(Roles = "admin, registeredUser")]
    public class OrdersController : Controller
    {
        private readonly AppCtx _context;
        private readonly UserManager<User> _userManager;

        public OrdersController(
            AppCtx context,
            UserManager<User> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: FormsOfStudy
        public async Task<IActionResult> Index()
        {
            // находим информацию о пользователе, который вошел в систему по его имени
            IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            // через контекст данных получаем доступ к таблице базы данных FormsOfStudy
            var appCtx = _context.Orders
                .Include(f => f.User) // и связываем с таблицей пользователи через класс User
                .Include(f => f.Game)  // устанавливается условие с выбором записей форм обучения текущего пользователя по его Id
                .OrderBy(f => f.OrderDate);          // сортируем все записи по имени форм обучения

            // возвращаем в представление полученный список записей
            return View(await appCtx.ToListAsync());
        }

        // GET: FormsOfStudy/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["IdGame"] = new SelectList(_context.Games
                 .OrderBy(f => f.NameGame), "Id", "NameGame");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderModel model)
        {
            IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);


            /*if (ModelState.IsValid)
            {*/
            Order order = new()
            {
                OrderDate = DateTime.Now,
                IdUser = user.Id,
                IdGame = model.IdGame,
                Count = model.Count
            };

            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            /* }
             ViewData["IdService"] = new SelectList(_context.ListServices.OrderBy(o => o.ServiceName), "Id", "ServiceName", model.IdService);
             return View(model);*/
        }

        // GET: FormsOfStudy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            EditOrderModel model = new()
            {
                Id = order.Id,
                OrderDate = DateTime.Now,
                IdUser = order.IdUser,
                IdGame = order.IdGame,
                Count = order.Count
            };

            ViewData["IdGame"] = new SelectList(_context.Games
                 .OrderBy(f => f.NameGame), "Id", "NameGame");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditOrderModel model)
        {
            Order order = await _context.Orders.FindAsync(id);

            if (id != order.Id)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {*/
            try
            {
                order.OrderDate = model.OrderDate;
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            return View(model);
        }

        // GET: FormsOfStudy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: FormsOfStudy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: FormsOfStudy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}