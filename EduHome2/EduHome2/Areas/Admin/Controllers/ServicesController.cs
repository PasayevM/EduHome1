using EduHome2.DAL;
using EduHome2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome2.Areas.Admin.Controllers
{ 

    [Area("Admin")]
    public class ServicesController : Controller
    {
        private readonly AppDbContext _db;
        public ServicesController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult>Index()
        {
            List<Service> services = await _db.Services.OrderByDescending(x=>x.Id).ToListAsync();
            return View(services);
        }
        public IActionResult Create()
        {
                return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

                bool isExist= await _db.Services.AnyAsync(x=>x.Title==service.Title);
                if (isExist) 
                    {
                        ModelState.AddModelError("Title", "This service is already exist");
                          return View();
                    }

            await _db.Services.AddAsync(service);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service service =await _db.Services.FirstOrDefaultAsync(x=>x.Id==id);
            if (service == null) 
            {
                return BadRequest();
            }
            return View(service);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service dbService = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (dbService == null)
            {
                return BadRequest();
            }
            return View(dbService);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Service service)
        {

            if (id == null)
            {
                return NotFound();
            }
            Service dbService = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (dbService == null)
            {
                return BadRequest();
            }
            bool isExist = await _db.Services.AnyAsync(x => x.Title == service.Title && x.Id!=id);
            if (isExist)
            {
                ModelState.AddModelError("Title", "This service is already exist");
                return View();
            }
            dbService.Title = service.Title;
            dbService.Description = service.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service dbService = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (dbService == null)
            {
                return BadRequest();
            }
            if (dbService.IsDeactive==true)
            {
                dbService.IsDeactive = false;

            }
            else
            {
                dbService.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
