using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MOVIES.Data;
using MOVIES.Models;

namespace MOVIES.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        [Authorize]
        public IActionResult Index()
        {
            var result=context.Categories.Include(c=>c.Movies).ToList();
            
            return View(result);
        }
        public IActionResult MoviesCategory(int id)
        {
            var result = context.Movies.Include(c => c.category).Where(c=>c.CategoryId== id).ToList();
            var categories = context.Movies.Include(c => c.Actors).Include(m => m.cinema).ToList();
            return result.IsNullOrEmpty() ? RedirectToAction("NotFound", "Home") : View(result);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(category);   
                context.SaveChanges();
                return RedirectToAction("Index","Category");
            }
            return View(category);
        }

    }
}
