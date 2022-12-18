using EduHome2.DAL;
using EduHome2.Helpers;
using EduHome2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeedBacksController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public FeedBacksController(AppDbContext db , IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<FeedBack> feedBacks = await _db.FeedBacks.ToListAsync();
            return View(feedBacks);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeedBack feedBack)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            #region Save Photo

            if (feedBack.Photo == null)
            {
                ModelState.AddModelError("Photo", "Bu hisse dolu olmalidir.");
                return View();
            }

            if (!feedBack.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Yalniz wekil formati.");
                return View();
            }

            if (feedBack.Photo.IsOlder2Mb())
            {
                ModelState.AddModelError("Photo", "Max 2MB.");
                return View();
            }

            string folder = Path.Combine(_env.WebRootPath, "img", "testimonial");
            feedBack.Image = await feedBack.Photo.SaveImageAsync(folder);

            #endregion

            await _db.FeedBacks.AddAsync(feedBack);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(); 
        }    
    }
}
