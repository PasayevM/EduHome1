using EduHome2.DAL;
using EduHome2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EduHome2.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _db;
        public CoursesController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Course> courses = _db.Courses.OrderByDescending(x=>x.Id).Take(6).ToList();
            ViewBag.CoursesCount = _db.Courses.Count(); 
            return View(courses);
        }
        public IActionResult LoadMore(int skip)
        {
            if(_db.Courses.Count() <= skip)
            {
                return Content("stop");
            }
            List<Course> courses = _db.Courses.OrderByDescending(x => x.Id).Skip(skip).Take(6).ToList();
            return PartialView("_LoadMoreCoursesPartial", courses);
        }
    }
}
