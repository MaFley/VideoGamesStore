using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using VideoGamesStore.Models;
using VideoGamesStore.Models.Data;
using VideoGamesStore.ViewModels.Games;

namespace VideoGamesStore.Controllers

{
    //[Authorize(Roles = "admin, registeredUser")]
    public class GamesController : Controller
    {
        private readonly AppCtx _context;
        private readonly UserManager<User> _userManager;

        public GamesController(AppCtx context, UserManager<User> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            //IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var appCtx = _context.Games
                .OrderBy(f => f.NameGame);

            return View(await appCtx.ToListAsync());
        }

        

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameModel model)
        {
        
             if (_context.Games
                .Where(f => f.NameGame == model.NameGame &&
                f.Country == model.Country &&
                f.GameDeveloper == model.GameDeveloper &&
                f.YearIssue == model.YearIssue &&
                f.GameDescription == model.GameDescription &&
                f.Platform == model.Platform)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеная игра уже существует");
            }

            if (ModelState.IsValid)
            {
                Game game = new()
                {
                    NameGame = model.NameGame,
                    Country = model.Country,
                    GameDeveloper = model.GameDeveloper,
                    YearIssue = model.YearIssue,
                    GameDescription = model.GameDescription,
                    Platform = model.Platform
                };

                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            EditGameModel model = new()
                {
                    Id = game.Id,
                    NameGame = game.NameGame,
                    Country = game.Country,
                    GameDeveloper = game.GameDeveloper,
                    YearIssue = game.YearIssue,
                    GameDescription = game.GameDescription,
                    Platform = game.Platform
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditGameModel model)
        {
            Game game = await _context.Games.FindAsync(id);

            if (_context.Games
                .Where(f => f.NameGame == model.NameGame && 
                f.Country == model.Country && 
                f.GameDeveloper == model.GameDeveloper && 
                f.YearIssue == model.YearIssue && 
                f.GameDescription == model.GameDescription && 
                f.Platform == model.Platform)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеная игра уже существует");
            }

            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    game.NameGame = model.NameGame;
                    game.Country = model.Country;
                    game.GameDeveloper = model.GameDeveloper;
                    game.YearIssue = model.YearIssue;
                    game.GameDescription = model.GameDescription;
                    game.Platform = model.Platform;
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Games == null)
            {
                return Problem("Entity set 'AppCtx.Games'  is null.");
            }
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
          return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }
    }
}
