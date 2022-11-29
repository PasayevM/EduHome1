using Microsoft.AspNetCore.Mvc;

namespace EduHome2.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
