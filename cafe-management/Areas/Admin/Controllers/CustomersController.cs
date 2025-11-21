//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using cafe_management.Models;
//using cafe_management.Models.Authentication;
//using X.PagedList;

//namespace cafe_management.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    [Route("Admin/Customers")]
//    public class CustomersController : Controller
//    {
//        private readonly CafeDBContext _context;

//        public CustomersController(CafeDBContext context)
//        {
//            _context = context;
//        }

//        [Route("")]
//        [Route("Index")]
//        [Authentication]
//        public IActionResult Index(int? page)
//        {
//            int pageSize = 30;
//            int pageNumber = page == null || page < 0 ? 1 : page.Value;
//            var listItem = _context.TbCustomers.AsNoTracking().OrderBy(x => x.PhoneNumber).ToList();
//            PagedList<TbCustomer> pagedListItem = new PagedList<TbCustomer>(listItem, pageNumber, pageSize);

//            return View(pagedListItem);
//        }

//        [Route("Search")]
//        [Authentication]
//        [HttpGet]
//        public IActionResult Search(int? page, string search)
//        {
//            int pageSize = 30;
//            int pageNumber = page == null || page < 0 ? 1 : page.Value;

//            if (string.IsNullOrEmpty(search))
//            {
//                return RedirectToAction("Index");
//            }

//            search = search.ToLower();
//            ViewBag.search = search;

//            var listItem = _context.TbCustomers.AsNoTracking().Where(x => x.Name.ToLower().Contains(search)).OrderBy(x => x.PhoneNumber).ToList();
//            PagedList<TbCustomer> pagedListItem = new PagedList<TbCustomer>(listItem, pageNumber, pageSize);

//            return View(pagedListItem);
//        }

//        [Route("Create")]
//        [Authentication]
//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [Route("Create")]
//        [Authentication]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(TbCustomer khachHang)
//        {
//            if (ModelState.IsValid)
//            {
//                // Nếu Id chưa có thì tạo mới (dù constructor đã làm rồi, thêm cho chắc)
//                if (khachHang.Id == Guid.Empty)
//                {
//                    khachHang.Id = Guid.NewGuid();
//                }

//                _context.TbCustomers.Add(khachHang);
//                _context.SaveChanges();

//                TempData["Message"] = "Thêm thành công";
//                return RedirectToAction("Index", "Customers");
//            }
//            return View(khachHang);

//            //_context.TbCustomers.Add(khachHang);
//            //_context.SaveChanges();

//            //TempData["Message"] = "Thêm thành công";

//            //return RedirectToAction("Index", "Customers");
//        }

//        //[Route("Edit")]
//        //[Authentication]
//        //[HttpGet]
//        //public IActionResult Edit(int id, string name)
//        //{
//        //    var khachHang = _context.TbCustomers.Find(id);
//        //    ViewBag.name = name;

//        //    return View(khachHang);
//        //}
//        [Route("Edit")]
//        [Authentication]
//        [HttpGet]
//        // SỬA: int id -> Guid? id
//        public IActionResult Edit(Guid? id)
//        {
//            if (id == null) return NotFound();

//            var khachHang = _context.TbCustomers.Find(id);
//            if (khachHang == null) return NotFound();

//            return View(khachHang);
//        }


//        [Route("Edit")]
//        [Authentication]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(TbCustomer khachHang)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Entry(khachHang).State = EntityState.Modified;
//                _context.SaveChanges();

//                TempData["Message"] = "Sửa thành công";
//                return RedirectToAction("Index", "Customers");
//            }
//            return View(khachHang);
//            //_context.Entry(khachHang).State = EntityState.Modified;
//            //_context.SaveChanges();

//            //TempData["Message"] = "Sửa thành công";

//            //return RedirectToAction("Index", "Customers");
//        }

//        [Route("Delete")]
//        [Authentication]
//        [HttpGet]
//        // SỬA: string id -> Guid? id
//        public IActionResult Delete(Guid? id)
//        {
//            if (id == null) return NotFound();

//            // 1. Kiểm tra xem khách hàng này có hóa đơn nào không?
//            // Lưu ý: Giả sử trong TbBill bạn có trường CustomerId là khóa ngoại trỏ về khách hàng
//            // Nếu tên trường khóa ngoại trong TbBill khác (vd: MaKhachHang), bạn hãy sửa chữ CustomerId bên dưới.
//            var hasBills = _context.TbBills.Any(x => x.CustomerId == id);

//            if (hasBills)
//            {
//                TempData["Message"] = "Không thể xóa: Khách hàng này đang có hóa đơn.";
//                return RedirectToAction("Index", "Customers");
//            }

//            // 2. Tìm và xóa
//            var khachHang = _context.TbCustomers.Find(id);
//            if (khachHang == null)
//            {
//                TempData["Message"] = "Không tìm thấy khách hàng";
//                return RedirectToAction("Index");
//            }

//            _context.TbCustomers.Remove(khachHang);
//            _context.SaveChanges();

//            TempData["Message"] = "Xoá thành công";
//            return RedirectToAction("Index", "Customers");
//        }

//        //[Route("Delete")]
//        //[Authentication]
//        //[HttpGet]
//        //public IActionResult Delete(string id)
//        //{
//        //    TempData["Message"] = "";

//        //    var hoaDon = _context.TbBills.Where(x => x.Id == Guid.Parse(id)).ToList();

//        //    if (hoaDon.Count() > 0)
//        //    {
//        //        TempData["Message"] = "Xoá không thành công";
//        //        return RedirectToAction("Index", "Customers");
//        //    }

//        //    _context.Remove(_context.TbCustomers.Find(id));
//        //    _context.SaveChanges();

//        //    TempData["Message"] = "Xoá thành công";

//        //    return RedirectToAction("Index", "Customers");
//        //}
//    }
//}


using cafe_management.Models;
using cafe_management.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace cafe_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/customers")]
    public class CustomersController : Controller
    {
        private readonly CafeDBContext _context;

        public CustomersController(CafeDBContext context)
        {
            _context = context;
        }

        // --- 1. INDEX & SEARCH GỘP LÀM 1 ---
        //[Route("")]
        //[Route("index")]
        //[Authentication]
        //public IActionResult Index(string search, int? page)
        //{
        //    int pageSize = 20; // Số lượng khách hàng mỗi trang
        //    int pageNumber = page == null || page < 0 ? 1 : page.Value;

        //    // Giữ lại từ khóa tìm kiếm
        //    ViewBag.SearchTerm = search;

        //    // Tạo query
        //    var query = _context.TbCustomers.AsNoTracking().AsQueryable();

        //    // Lọc dữ liệu nếu có tìm kiếm (theo Tên hoặc SĐT)
        //    if (!String.IsNullOrEmpty(search))
        //    {
        //        string lowerSearch = search.ToLower();
        //        query = query.Where(x => x.Name.ToLower().Contains(lowerSearch) ||
        //                                 x.PhoneNumber.Contains(lowerSearch));
        //    }

        //    // Sắp xếp theo tên
        //    var pagedList = query.OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize);

        //    return View(pagedList);
        //}
        [Route("")]
        [Route("index")]
        [Authentication]
        public IActionResult Index(string search, int? page)
        {
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            // Giữ lại từ khóa để hiển thị ở View
            ViewBag.SearchTerm = search;

            // 1. Tạo query cơ bản
            var query = _context.TbCustomers.AsNoTracking().AsQueryable();

            // 2. Xử lý tìm kiếm GẦN ĐÚNG (Contains)
            if (!String.IsNullOrEmpty(search))
            {
                // Chuyển về chữ thường để tìm không phân biệt hoa/thường
                string lowerSearch = search.Trim().ToLower();

                // Logic: Tên chứa từ khóa HOẶC Số điện thoại chứa từ khóa
                query = query.Where(x => x.Name.ToLower().Contains(lowerSearch) ||
                                         x.PhoneNumber.Contains(lowerSearch));
            }

            // 3. Sắp xếp và phân trang
            var pagedList = query.OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }

        // --- 2. CREATE ---
        [Route("Create")]
        [HttpGet]
        [Authentication]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        [Authentication]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbCustomer khachHang)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Nếu ID chưa có, tự tạo Guid mới
                    if (khachHang.Id == Guid.Empty)
                    {
                        khachHang.Id = Guid.NewGuid();
                    }

                    _context.TbCustomers.Add(khachHang);
                    _context.SaveChanges();

                    TempData["MessageType"] = "success";
                    TempData["Message"] = "Thêm khách hàng thành công";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi hệ thống: " + ex.Message);
                }
            }
            return View(khachHang);
        }

        // --- 3. EDIT ---
        [Route("Edit")]
        [HttpGet]
        [Authentication]
        public IActionResult Edit(Guid id)
        {
            // Vì là Guid nên dùng Find trực tiếp
            var khachHang = _context.TbCustomers.Find(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        [Route("Edit")]
        [HttpPost]
        [Authentication]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbCustomer khachHang)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(khachHang).State = EntityState.Modified;
                    _context.SaveChanges();

                    TempData["MessageType"] = "success";
                    TempData["Message"] = "Cập nhật khách hàng thành công";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi cập nhật: " + ex.Message);
                }
            }
            return View(khachHang);
        }

        // --- 4. DELETE ---
        [Route("Delete")]
        [HttpGet]
        [Authentication]
        public IActionResult Delete(Guid id)
        {
            // 1. Kiểm tra ràng buộc: Khách hàng này có hóa đơn nào không?
            // Dùng Any() tối ưu hơn Count()
            bool hasBills = _context.TbBills.Any(x => x.CustomerId == id);

            if (hasBills)
            {
                TempData["MessageType"] = "danger";
                TempData["Message"] = "Không thể xóa: Khách hàng này đã có lịch sử mua hàng.";
                return RedirectToAction("Index");
            }

            // 2. Tìm khách hàng
            var khachHang = _context.TbCustomers.Find(id);
            if (khachHang == null)
            {
                TempData["MessageType"] = "danger";
                TempData["Message"] = "Không tìm thấy khách hàng.";
                return RedirectToAction("Index");
            }

            // 3. Xóa
            try
            {
                _context.TbCustomers.Remove(khachHang);
                _context.SaveChanges();

                TempData["MessageType"] = "success";
                TempData["Message"] = "Xóa khách hàng thành công";
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = "danger";
                TempData["Message"] = "Lỗi khi xóa: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}