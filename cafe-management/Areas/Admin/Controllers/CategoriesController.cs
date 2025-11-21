using cafe_management.Models;
using cafe_management.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;
// Không cần using X.PagedList.Extensions;

//note: bug edit category

namespace cafe_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/categories")]
    public class CategoriesController : Controller
    {
        private readonly CafeDBContext _context;

        public CategoriesController(CafeDBContext context)
        {
            _context = context;
        }

        // 1. ĐÃ GỘP INDEX VÀ SEARCH VÀO LÀM MỘT
        [Route("")]
        [Route("index")]
        [Authentication]
        public IActionResult Index(string search, int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            // Giữ lại tham số tìm kiếm
            ViewBag.SearchTerm = search;

            // Bắt đầu câu truy vấn (chưa thực thi)
            IQueryable<TbCategory> query = _context.TbCategories.AsNoTracking();

            // Nếu có tìm kiếm, thêm điều kiện Where
            if (!String.IsNullOrEmpty(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(lowerSearch));
            }

            // 2. TỐI ƯU PHÂN TRANG:
            // Sắp xếp VÀ phân trang trên IQueryable (chạy ở database)
            var pagedListItem = query.OrderBy(p => p.Name).ToPagedList(pageNumber, pageSize);

            return View(pagedListItem);
        }

        [Route("Create")]
        [HttpGet]
        [Authentication]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbCategory category)
        {
            // 3. THÊM KIỂM TRA MODELSTATE
            if (ModelState.IsValid)
            {
                _context.TbCategories.Add(category);
                _context.SaveChanges();

                TempData["MessageType"] = "success";
                TempData["Message"] = "Thêm loại sản phẩm thành công";

                return RedirectToAction("Index", "Categories");
            }
            // Nếu không hợp lệ, quay lại View với dữ liệu đã nhập
            return View(category);
        }

        [Route("Edit")]
        [HttpGet]
        [Authentication]
        public IActionResult Edit(int id) // Bỏ 'string name' không cần thiết
        {
            var category = _context.TbCategories.Find(id);
            if (category == null)
            {
                return NotFound(); // Thêm kiểm tra nếu không tìm thấy
            }
            return View(category);
        }

        [Route("Edit")]
        [HttpPost]
        [Authentication]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbCategory category)
        {
            // 3. THÊM KIỂM TRA MODELSTATE
            if (ModelState.IsValid)
            {
                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();

                TempData["MessageType"] = "success";
                TempData["Message"] = "Cập nhật thành công";

                return RedirectToAction("Index", "Categories");
            }
            // Nếu không hợp lệ, quay lại View với dữ liệu đã nhập
            return View(category);
        }

        // 4. SỬA LỖI LOGIC NGHIÊM TRỌNG TRONG HÀM DELETE
        [Route("Delete")]
        [HttpGet]
        [Authentication]
        public IActionResult Delete(int id)
        {
            // Mặc định là lỗi
            TempData["MessageType"] = "danger";

            // Bước 1: Kiểm tra xem có SẢN PHẨM nào thuộc loại này không
            // (Giống như bạn kiểm tra BillDetails trước khi xóa Product)
            var relatedProducts = _context.TbProducts.Where(x => x.CategoryId == id).ToList();

            if (relatedProducts.Count > 0)
            {
                TempData["Message"] = "Không thể xóa loại này, vì vẫn còn sản phẩm đang sử dụng.";
                return RedirectToAction("Index", "Categories");
            }

            // Bước 2: Nếu không có sản phẩm liên quan, tiến hành xóa
            var category = _context.TbCategories.Find(id);
            if (category == null)
            {
                TempData["Message"] = "Không tìm thấy loại sản phẩm này.";
                return RedirectToAction("Index", "Categories");
            }

            // Bước 3: Thực hiện xóa
            _context.Remove(category);
            _context.SaveChanges();

            // Nếu thành công, đổi MessageType
            TempData["MessageType"] = "success";
            TempData["Message"] = "Xóa loại sản phẩm thành công";

            return RedirectToAction("Index", "Categories");
        }
    }
}

