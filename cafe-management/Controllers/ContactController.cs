using cafe_management.Models;
using Microsoft.AspNetCore.Mvc;

namespace cafe_management.Controllers
{
    public class ContactController : Controller
    {

        private readonly CafeDBContext _context;

        public ContactController(CafeDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Model changed from TbPhanHoi -> TbFeedback
        // Variable changed from phanHoi -> feedback
        public IActionResult Index(TbFeedback feedback)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // DbSet changed from TbPhanHois -> TbFeedbacks
                    _context.TbFeedbacks.Add(feedback);
                    _context.SaveChanges();
                    TempData["Status"] = "success";
                    // Message translated
                    TempData["Message"] = "Feedback submitted successfully.";
                    return RedirectToAction("index");
                }
                catch
                {
                    TempData["Status"] = "error";
                    TempData["Message"] = "Failed to submit feedback.";
                }
            }

            // Return the object in case of validation failure
            return View(feedback);
        }
    }
}