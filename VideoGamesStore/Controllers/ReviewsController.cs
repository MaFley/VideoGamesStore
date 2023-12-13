using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VideoGamesStore.Models;
using VideoGamesStore.Models.Data;
using VideoGamesStore.ViewModels.Reviews;

namespace VideoGamesStore.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AppCtx _context;
        private readonly UserManager<User> _userManager;
        public enum RatingList
        {
            [Display(Name = "Выбери оценку")]
            SelectRating,
            Плохо = 1,
            Удовлетворительно = 2,
            Нормально = 3,
            Хорошо = 4,
            Отлично = 5
        }
        public ReviewsController(AppCtx context, UserManager<User> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var reviews = from s in _context.Reviews
                        select s;

            IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var appCtx = _context.Review
                .Include(r => r.Game)
                .Include(r => r.User);

            int pageSize = 3;
            return View(await PaginatedList<Review>.CreateAsync(reviews.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Review == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.Game)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public async Task<IActionResult> CreateAsync()
        {

            IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            ViewData["IdGame"] = new SelectList(_context.Games
                 .OrderBy(f => f.NameGame), "Id", "NameGame");
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReviewViewModel model)
        {
            ViewData["IdGame"] = new SelectList(_context.Games, "Id", "NameGame", model.IdGame);
            /*if (ModelState.IsValid)
            {*/
            IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                Review review = new()
                {
                    Rating = model.Rating,
                    ReviewText = model.ReviewText,
                    ReviewDateTime = DateTime.Now.Date,
                    IdUser = user.Id,
                    IdGame = model.IdGame,
                };
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            /*}
            
            return View(model);*/
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            EditReviewViewModel model = new()
            {
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                ReviewDateTime = review.ReviewDateTime,
                IdUser = review.IdUser,
            };

            IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["IdGame"] = new SelectList(_context.Games, "Id", "NameGame", review.IdGame);
            
            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditReviewViewModel model)
        {
            Review review = await _context.Reviews.FindAsync(id);

            if (_context.Reviews
                .Where(f => f.IdUser == model.IdUser)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Этот пользователь уже оставлял отзыв");
            }
            if (id != review.Id)
            {
                return NotFound();
            }
            /*if (ModelState.IsValid)
            {*/
            try
            {
                review.Rating = ViewBag.rtg;
                review.ReviewText = model.ReviewText;
                review.ReviewDateTime = DateTime.Now.Date;
                review.IdUser = model.IdUser;
                review.IdGame = model.IdGame;
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(review.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            return View(model);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Review == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.Game)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Review == null)
            {
                return Problem("Entity set 'AppCtx.Review'  is null.");
            }
            var review = await _context.Review.FindAsync(id);
            if (review != null)
            {
                _context.Review.Remove(review);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(short id)
        {
          return (_context.Review?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
