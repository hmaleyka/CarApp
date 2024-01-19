using CarvillaApp.Context;
using CarvillaApp.Helpers;
using CarvillaApp.Models;
using CarvillaApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarvillaApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                Name = register.Name,
                Email = register.Email,
                Surname = register.Surname,
                UserName = register.Username
            };

            var result = await _userManager.CreateAsync(user , register.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }           
            }

            await _userManager.AddToRoleAsync(user , UserRole.Admin.ToString());
            return RedirectToAction("Login");

        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            var user = await _userManager.FindByEmailAsync(login.EmailOrUsername);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(login.EmailOrUsername);
                if (user is null)
                {
                    ModelState.AddModelError("", "Username-email or passwor is incorrect");
                    return View();
                }
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password , false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username-email or passwor is incorrect");
                return View();
            }

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction(nameof(Index), "Home");

        }

        public async Task<IActionResult> CreateRole()
        {
            foreach (UserRole item in Enum.GetValues(typeof(UserRole)))
            {
                if(await _roleManager.FindByNameAsync(item.ToString())==null)
                {
                    await _roleManager.CreateAsync(new IdentityRole()
                    {
                        Name= item.ToString(),
                    });                   
                }
            }
            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home"); 
        }


    }
}
