using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MOVIES.Data;
using MOVIES.Data.Enums;
using MOVIES.Migrations;
using MOVIES.Models;
using System.Diagnostics.Metrics;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Authorization;



namespace MOVIES.Controllers
{
    public class MovieController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
         //increamrnt views
        public IActionResult MovieDetails(int id)
        {
               

            //if (HttpContext.Session.GetInt32("viewcount")==null)
            //{

            //    HttpContext.Session.SetInt32("viewcount" , 0);
            //}
            
            
                var result = context.Movies.Find(id);
            if (result == null)
            {
                return RedirectToAction("Notfount", "Home");
            }
                var cinema = context.Movies.Include(m => m.cinema).ToList();
                var category = context.Movies.Include(m => m.category).ToList();
                var Actors = context.Movies.Include(a => a.Actors).ToList();
            
            result.viewcount++;
            context.SaveChanges();  
            ViewBag.count = result.viewcount;
            //    var session=(int)HttpContext.Session.GetInt32("viewcount");
            //if (session != null)
            //{
            //    HttpContext.Session.SetInt32("viewcount", session + 1);
            //    ViewBag.count = HttpContext.Session.GetInt32("viewcount");
            //}
                return View(result);
                
            
            //return RedirectToAction("Notfount", "Home");

            
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var resultCategory = context.Categories.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();
            ViewBag.ListofCategory = resultCategory;
            var resultCinma = context.Cinemas.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();

            ViewBag.ListofCinema = resultCinma;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if(ModelState.IsValid) 
            {
                if(movie.StartDate <= DateTime.Today && movie.EndDate > DateTime.Today)
                {
                    movie.MovieStatus =MovieStatus.Available;
                }
                else if(movie.StartDate > DateTime.Today && movie.EndDate > DateTime.Today)
                {
                    movie.MovieStatus = MovieStatus.Upcoming;
                }
                else
                {
                    movie.MovieStatus = MovieStatus.Expired;
                }
                context.Movies.Add(movie);
                context.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(movie);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var resultCategory = context.Categories.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();
            ViewBag.ListofCategory = resultCategory;
            var resultCinma = context.Cinemas.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();
            ViewBag.ListofCinema = resultCinma;
            var result = context.Movies.Find(id);
            ViewBag.count = result.viewcount;
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (movie.StartDate <= DateTime.Today && movie.EndDate > DateTime.Today)
                {
                    movie.MovieStatus = MovieStatus.Available;
                }
                else if (movie.StartDate > DateTime.Today && movie.EndDate > DateTime.Today)
                {
                    movie.MovieStatus = MovieStatus.Upcoming;
                }
                else
                {
                    movie.MovieStatus = MovieStatus.Expired;
                }
                context.Movies.Update(movie);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(movie);
        }
    }

}
