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

        //public IActionResult Index(int? page)
        //{
        //    int pageSize = 18;
        //    int pageNumber = page == null || page < 0 ? 1 : page.Value;
        //    var listItem = _context.TbProducts.AsNoTracking().OrderBy(x => x.Id).ToList();
        //    PagedList<TbProduct> pagedListItem = new PagedList<TbProduct>(listItem, pageNumber, pageSize);

        //    return View(pagedListItem);
        //}
        //public IActionResult Index(int? page, string search)
        //{
        //    int pageSize = 9;
        //    int pageNumber = page == null || page < 0 ? 1 : page.Value;

        //    // Tạo query ban đầu
        //    var query = _context.TbProducts.AsNoTracking().AsQueryable();

        //    // Xử lý tìm kiếm
        //    if (!string.IsNullOrEmpty(search))
        //    {
        //        query = query.Where(x => x.Name.Contains(search));
        //        ViewBag.search = search; // <-- QUAN TRỌNG: Truyền lại từ khóa sang View
        //    }

        //    // Sắp xếp và phân trang
        //    var listItems = query.OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize);

        //    return View(listItems);
        //}

        //public IActionResult Index(int? page, string search, int? categoryID)
        //{
        //    int pageSize = 9;
        //    int pageNumber = page == null || page < 0 ? 1 : page.Value;

        //    // 1. Tạo query cơ bản
        //    var query = _context.TbProducts.AsNoTracking().AsQueryable();

        //    // 2. Xử lý TÌM KIẾM (nếu có)
        //    if (!string.IsNullOrEmpty(search))
        //    {
        //        query = query.Where(x => x.Name.Contains(search));
        //        ViewBag.search = search; // Lưu lại để View dùng
        //    }

        //    // 3. Xử lý LỌC THEO LOẠI (nếu có)
        //    if (categoryID.HasValue && categoryID > 0)
        //    {
        //        query = query.Where(x => x.CategoryId == categoryID);

        //        ViewBag.categoryID = categoryID; // Lưu lại ID để phân trang

        //        // Lấy tên loại để hiển thị lên tiêu đề (Breadcrumb/Title)
        //        var category = _context.TbCategories.Find(categoryID);
        //        if (category != null)
        //        {
        //            ViewBag.CategoryName = category.Name;
        //        }
        //    }

        //    // 4. Sắp xếp và phân trang
        //    var listItems = query.OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize);

        //    return View(listItems);
        //}


        // Action chính (chỉ tải khung trang)
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


        //public IActionResult Type(int target, string targetName, int? page)
        //{
        //    int pageSize = 9;
        //    int pageNumber = page == null || page < 0 ? 1 : page.Value;
        //    var listItem = _context.TbProducts.AsNoTracking().Where(x => x.CategoryId == target).OrderBy(x => x.Name).ToList();
        //    PagedList<TbProduct> pagedListItem = new PagedList<TbProduct>(listItem, pageNumber, pageSize);

        //    ViewBag.target = target;
        //    ViewBag.targetName = targetName;

        //    return View(pagedListItem);
        //}

        public IActionResult Details(int id)
        {
            var products = _context.TbProducts.SingleOrDefault(x => x.Id == id);

            return View(products);
        }
    }
}
