using cafe_management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace cafe_management.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CafeDBContext _context;

        public ProductsController(CafeDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 18;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = _context.TbProducts.AsNoTracking().OrderBy(x => x.Id).ToList();
            PagedList<TbProduct> pagedListItem = new PagedList<TbProduct>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        public IActionResult Type(int target, string targetName, int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = _context.TbProducts.AsNoTracking().Where(x => x.CategoryId == target).OrderBy(x => x.Name).ToList();
            PagedList<TbProduct> pagedListItem = new PagedList<TbProduct>(listItem, pageNumber, pageSize);

            ViewBag.target = target;
            ViewBag.targetName = targetName;

            return View(pagedListItem);
        }

        public IActionResult Details(int id)
        {
            var products = _context.TbProducts.SingleOrDefault(x => x.Id == id);

            return View(products);
        }
    }
}
