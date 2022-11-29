using Microsoft.AspNetCore.Mvc;

namespace EduHome2.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
