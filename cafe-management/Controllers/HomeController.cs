using cafe_management.Models;
using cafe_management.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace Web_CuaHangCafe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CafeDBContext _context;

        public HomeController(ILogger<HomeController> logger, CafeDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel value = new HomeViewModel();
            var lstProducts = _context.TbProducts.AsNoTracking().OrderBy(x => x.Id).Take(8).ToList();
            var lstNews = _context.TbNews.AsNoTracking().OrderByDescending(x => x.PostedDate).Take(3).ToList();
            value.lstSanPham = lstProducts;
            value.lstTinTuc = lstNews;

            return View(value);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}