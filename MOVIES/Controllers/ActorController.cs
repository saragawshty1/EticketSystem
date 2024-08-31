using Microsoft.AspNetCore.Mvc;
using MOVIES.Data;

namespace MOVIES.Controllers
{
    public class ActorController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult ActorDetails(int id)
        {
            var result = context.Actors.Find(id);
            return View(result);
        }
    }
}
