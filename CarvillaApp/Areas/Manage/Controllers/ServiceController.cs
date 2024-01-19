using CarvillaApp.Areas.ViewModels;
using CarvillaApp.Context;
using CarvillaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarvillaApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [AutoValidateAntiforgeryToken]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            List<Service> services = _context.services.ToList();
            return View(services);
        }
		[Authorize(Roles = "Admin")]
		public IActionResult Create ()
        {
            return View();
        }
		[Authorize(Roles = "Admin")]
		[HttpPost]
        public async Task<IActionResult> Create(CreateServiceVM servicevm)
        {
			if (!ModelState.IsValid)
			{
				return View();
			}

			Service service = new Service()
            {
                Title = servicevm.Title,
                Description = servicevm.Description,
                Icon = servicevm.Icon,
            };
            await _context.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update(int id)
        {
            Service service= await _context.services.FirstOrDefaultAsync(x=>x.Id==id);
            UpdateServiceVM services = new UpdateServiceVM()
            {
                Title = service.Title,
                Description = service.Description,
                Icon = service.Icon,
            };
            return View(services);
        }
		[Authorize(Roles = "Admin")]
		[HttpPost]
        public async Task<IActionResult> Update(UpdateServiceVM servicevm)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var service = await _context.services.Where(x=>x.Id== servicevm.Id).FirstOrDefaultAsync();
            if(service!=null)
            {
                service.Title = servicevm.Title;
                service.Description = servicevm.Description;
                service.Icon = servicevm.Icon;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int id)
        {
            Service service = _context.services.Where(x=>x.Id==id).FirstOrDefault();
                _context.Remove(service);
                _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }

    }
}
