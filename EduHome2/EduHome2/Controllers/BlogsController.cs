using Microsoft.AspNetCore.Mvc;

namespace EduHome2.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
