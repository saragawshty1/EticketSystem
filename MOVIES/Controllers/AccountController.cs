using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MOVIES.Models;
using MOVIES.Models.ViewModel;
using NuGet.ContentModel;

namespace MOVIES.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> usermanger;
        SignInManager<ApplicationUser> SignInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> usermanger, SignInManager<ApplicationUser> SignInManager,RoleManager<IdentityRole>roleManager) 
        {
            this.usermanger = usermanger;
            this.SignInManager = SignInManager;
            this.roleManager = roleManager;
        }

         

        public IActionResult Register()
        {
            if (User.IsInRole("Admin"))
            {
             var result=  roleManager.Roles.Select(e => new SelectListItem
                {
                    Value = e.Name,
                    Text = e.Name
                });
                ViewBag.Roles = result;
            }
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Register(ApplicationUserVM userVM)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = userVM.UserName,
                    Email = userVM.Email,
                     Address = userVM.Address
                };
                var result = await usermanger.CreateAsync(user,userVM.Password);
                if (result.Succeeded)
                {
                    if (User.IsInRole("Admin"))
                        await usermanger.AddToRoleAsync(user, userVM.Role);
                    else
                        await usermanger.AddToRoleAsync(user, "Customer");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "password not match");
                }

            }


            return View(userVM);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var user=await usermanger.FindByEmailAsync(loginVM.Email);
                if(user!=null)
                {
                    var result=await usermanger.CheckPasswordAsync(user,loginVM.Password);
                    if (result)
                    {
                        await SignInManager.SignInAsync(user, loginVM.RememberMe);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                        ModelState.AddModelError("Password", "incorrect password");
                }
                else
                    ModelState.AddModelError("Email", "incorrect email");

            }
            return View(loginVM);
        }
        public IActionResult AccessDenied()
        {
            return RedirectToAction("NotFound", "Home");
        }
        public IActionResult Logout()
        {
            SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult >CreateRole(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole user = new  (roleVM.Name);
                await roleManager.CreateAsync(user);
                return RedirectToAction( "CreateRole");
            }
            return View(roleVM);
        }
    }
}
