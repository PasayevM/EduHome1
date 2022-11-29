using Microsoft.AspNetCore.Mvc;

namespace EduHome2.Controllers
{
    public class TeachersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
