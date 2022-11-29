using EduHome2.DAL;
using EduHome2.Models;
using EduHome2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            HomeVM homeVM = new HomeVM
            {
                Sliders = _db.Sliders.ToList(),
                Services = _db.Services.Where(x => x.IsDeactive == false).ToList(),
                Abouts = _db.Abouts.First(),
                Courses = _db.Courses.OrderByDescending(x=>x.Id).Take(3).ToList(),
                FeedBacks = _db.FeedBacks.First(),
            };
              return View(homeVM);
        }

       
       
        public IActionResult Error()
        {
            return View();
        }
    }
}
