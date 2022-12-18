using EduHome2.DAL;
using EduHome2.Helpers;
using EduHome2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SlidersController(AppDbContext db , IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _db.Sliders.ToListAsync();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            #region Save Photo

            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo", "Bu hisse dolu olmalidir.");
                return View();
            }

            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Yalniz wekil formati.");
                return View();
            }

            if (slider.Photo.IsOlder2Mb())
            {
                ModelState.AddModelError("Photo", "Max 2MB.");
                return View();
            }

            string folder = Path.Combine(_env.WebRootPath , "img" , "slider");
            slider.Image =  await slider.Photo.SaveImageAsync(folder);

            #endregion

            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return BadRequest();
            }
            return View(slider);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSlider == null)
            {
                return BadRequest();
            }
            return View(dbSlider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSlider == null)
            {
                return BadRequest();
            }

            #region Save Photo Update

            if (slider.Photo != null)
            {
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Yalniz wekil formati.");
                    return View();
                }

                if (slider.Photo.IsOlder2Mb())
                {
                    ModelState.AddModelError("Photo", "Max 2MB.");
                    return View();
                }

                string folder = Path.Combine(_env.WebRootPath, "img", "slider");
                string path = Path.Combine(folder, dbSlider.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbSlider.Image = await slider.Photo.SaveImageAsync(folder);
            }
           

            #endregion

            dbSlider.Title = slider.Title;
            dbSlider.Description = slider.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSlider == null)
            {
                return BadRequest();
            }
            if (dbSlider.IsDeactive == true)
            {
                dbSlider.IsDeactive = false;

            }
            else
            {
                dbSlider.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
