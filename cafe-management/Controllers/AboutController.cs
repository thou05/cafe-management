using Microsoft.AspNetCore.Mvc;

namespace cafe_management.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
