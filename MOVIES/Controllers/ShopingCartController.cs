using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOVIES.Data;
using MOVIES.Migrations;
using MOVIES.Models;
using Newtonsoft.Json;
using Stripe.Checkout;
using Stripe.Climate;
//using Stripe.Forwarding;

namespace MOVIES.Controllers
{
    public class ShopingCartController : Controller

    {
        ApplicationDbContext context = new ApplicationDbContext();
        private UserManager<ApplicationUser> usermanger;

        public ShopingCartController(UserManager<ApplicationUser> usermanger)
        {
            this.usermanger = usermanger;

        }
        [Authorize]
        public IActionResult Index(int id)

        {
            var userid = usermanger.GetUserId(User);
            if (id != 0)
            {
                MovieCart movieCart = new MovieCart()
                {
                    MovieId = id,
                    ApplicationUserId = userid,
                    Count = 1,
                   
                };
                context.MovieCarts.Add(movieCart);
                context.SaveChanges();
                TempData["add"] = context.MovieCarts.Include(e => e.Movie).Where(e => e.MovieId == id).Select(e => e.Movie.Name).FirstOrDefault();
                 
                return RedirectToAction("Index", "Home");
            }
            var result = context.MovieCarts.Include(e => e.Movie)
                .Where(e => e.ApplicationUserId == userid).ToList();
            TempData["shoppingCart"] = JsonConvert.SerializeObject(result);
            
            ViewBag.total = ((float)result.Sum(e => e.Count * e.Movie.Price));
           
            return View(result);
        }
        public IActionResult Increamnt(int id)
        {
            var result = context.MovieCarts.Find(id);
            result.Count++;
            context.SaveChanges();
            return RedirectToAction("Index", "ShopingCart");
        }
        public IActionResult Decreamnt(int id)
        {
            var result = context.MovieCarts.Find(id);
            if (result.Count == 1)
                context.MovieCarts.Remove(result);
            else
                result.Count--;
            context.SaveChanges();
            return RedirectToAction("Index", "ShopingCart");
        }
        public IActionResult Delete(int id)
        {
            var result = context.MovieCarts.Find(id);
            context.MovieCarts.Remove(result);
            context.SaveChanges();
            return RedirectToAction("Index", "ShopingCart");
        }


        public IActionResult Pay()
        {
            var items = JsonConvert.DeserializeObject<IEnumerable<MovieCart>>((string)TempData["shoppingCart"]);
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };
            foreach (var model in items)
            {
                var result = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = model.Movie.Name,
                        },
                        UnitAmount = (long)model.Movie.Price * 100,
                    },
                    Quantity = model.Count,
                };
                options.LineItems.Add(result);
            }

            var service = new SessionService();
            var session = service.Create(options);
            if (session !=null)
            {
                 
                var userid = usermanger.GetUserId(User);

                // Retrieve items from the cart
                var cartItems = context.MovieCarts.Include(e => e.Movie)
                                                  .Where(e => e.ApplicationUserId == userid)
                                                  .ToList();

                // Create the order
                var order = new order
                {
                    ApplicationUserId = userid,
                    OrderDate = DateTime.Now,
                    PaymentStatus = "Paid",
                    // Create a list of OrderItems if needed for in-memory representation
                    OrderItems = new List<MovieCart>()
                };

                // Add cart items to the order and update MovieCart items
                foreach (var item in cartItems)
                {
                    // Associate the MovieCart item with the new order
                  item.orderId = order.Id; // Set OrderId to link the item to the order
                    order.OrderItems.Add(item); // Add item to the in-memory order's items
                }

                // Add the order to the context and save changes
                context.orders.Add(order);
                 context.SaveChanges (); // Save to get the order's Id

                // Update MovieCart items with the correct OrderId
                foreach (var item in cartItems)
                {
                    item.orderId = order.Id; // Set OrderId to the new order's Id
                    item.status = "paid order";
                }
                // Save changes to MovieCarts
                context.MovieCarts.UpdateRange(cartItems);
              context.SaveChanges ();
                
                
                //// Clear the shopping cart
                //context.MovieCarts.RemoveRange(cartItems);
                //context.SaveChanges();

            }
            return Redirect(session.Url);

        }
       public IActionResult Orders()
        {
            var userid = usermanger.GetUserId(User);
            // Get the logged-in user's ID
            var orders = context.orders
                        
                        .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Movie)
                        .ToList();


            return View(orders);
        }
    }

}