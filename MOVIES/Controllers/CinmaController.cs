using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MOVIES.Data;
 
using MOVIES.Models;

namespace MOVIES.Controllers
{
    public class CinmaController : Controller
    {
        ApplicationDbContext context=new ApplicationDbContext();
        [Authorize]
        public IActionResult Index()
        {
            var result=context.Cinemas.Include(m=>m.Movies).ToList();   
            return View(result);
        }
        public IActionResult MoviesCinema(int id)
        {
            var result = context.Movies.Include(c => c.cinema).Where(c => c.CinemaId == id).ToList();
            var categories = context.Movies.Include(c => c.Actors).Include(m => m.category).ToList();
            return result.IsNullOrEmpty() ? RedirectToAction("NotFound", "Home") : View(result);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                context.Cinemas.Add(cinema);
                context.SaveChanges();
                return RedirectToAction("Index", "Cinma");
            }
            return View(cinema);
        }
    }
}
