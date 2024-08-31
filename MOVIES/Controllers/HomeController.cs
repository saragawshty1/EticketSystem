using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MOVIES.Data;
using MOVIES.Models;
using System.Diagnostics;

namespace MOVIES.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        ApplicationDbContext context = new ApplicationDbContext();
        //public IActionResult Index(int? CategoryId=null )
        //{
        //    var result = context.Movies.Include(m => m.cinema).Include(m => m.category).ToList();
        //    if (CategoryId !=null)
        //    {
        //        result = context.Movies.Include(m => m.cinema).Include(m => m.category).Where(m => m.CategoryId == CategoryId).ToList();
        //        ViewBag.IsSelected = true;
        //    }
        //    else
        //    {
        //        ViewBag.IsSelected = false;
        //    }

        //    return View(result);
        //}
        public IActionResult Index()

        {
             
            var result = context.Movies.Include(m => m.cinema).Include(m => m.category).Include(m=>m.Actors).ToList();



            return View(result);
        }
        [HttpGet]
        public IActionResult SearchMovies(string searchTerm)
        {
            var result = context.Movies.Where(e => e.Name.Contains(searchTerm)).Include(m => m.cinema).Include(m => m.category).ToList();

            return result.IsNullOrEmpty() ? RedirectToAction("NotFound", "Home") : View("Index",result);
        }

        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
