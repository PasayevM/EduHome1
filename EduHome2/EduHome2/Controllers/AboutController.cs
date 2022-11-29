using Microsoft.AspNetCore.Mvc;

namespace EduHome2.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
