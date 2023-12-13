using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGamesStore.Models.Data;
using VideoGamesStore.ViewModels.Users;

namespace VideoGamesStore.Controllers
{
    public class UsersController : Controller
    {
        UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // отображение списка пользователей
        // действия для начальной страницы Index
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            //NameUser Surname RegDate
            ViewData["NameUserSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nameUser_desc" : "";
            ViewData["SurnameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "surname_desc" : "";
            ViewData["RegDateSortParm"] = sortOrder == "Date" ? "regDate_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var users = from u in _userManager.Users
                        select u;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.NickName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nameUser_desc":
                    users = users.OrderByDescending(u => u.NickName);
                    break;
                case "regDate_desc":
                    users = users.OrderByDescending(u => u.DateOfReg);
                    break;
                default:
                    users = users.OrderBy(u => u.NickName);
                    break;
            }
            return View(await users.AsNoTracking().ToListAsync());
        }


        // действия для создания пользователя Create
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    NickName = model.NickName,
                    DateOfReg = DateTime.Today,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }


        // действия для изменения пользователя Edit
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                NickName = user.NickName,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.NickName = model.NickName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }


        // действия для удаления пользователя Delete с подтверждением
        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            IdentityResult result = await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
