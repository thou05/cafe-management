using cafe_management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace cafe_management.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CafeDBContext _context;

        public ProductsController(CafeDBContext context)
        {
            _context = context;
        }

       
        public IActionResult Index(string search, int? categoryID)
        {
            // Lưu lại các tham số này để truyền xuống View
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentCategoryID = categoryID;

            return View();
        }

        // Action cho AJAX (trả về HTML danh sách sản phẩm)
        public IActionResult GetProductList(int? page, string search, int? categoryID)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;

            var query = _context.TbProducts.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            if (categoryID.HasValue && categoryID > 0)
            {
                query = query.Where(x => x.CategoryId == categoryID);
            }

            var listItems = query.OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize);

            // Lưu lại tham số để dùng trong phân trang ở Partial View
            ViewBag.currentFilter = search;
            ViewBag.currentCateID = categoryID;

            return PartialView("_ProductListPartial", listItems);
        }


        public IActionResult Details(int id)
        {
            var products = _context.TbProducts.SingleOrDefault(x => x.Id == id);

            return View(products);
        }
    }
}
