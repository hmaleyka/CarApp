using CarvillaApp.Context;
using CarvillaApp.Models;
using CarvillaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarvillaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homevm = new HomeVM()
            {
                services = _context.services.ToList(),
            };

            return View(homevm);
        }

        
       
    }
}