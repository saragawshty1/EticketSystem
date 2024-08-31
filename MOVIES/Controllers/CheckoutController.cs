using Microsoft.AspNetCore.Mvc;

namespace MOVIES.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
         
            public IActionResult success()
            {
                return View();
            }
            public IActionResult cancel()
            {
                return View();
            }
        }
}
