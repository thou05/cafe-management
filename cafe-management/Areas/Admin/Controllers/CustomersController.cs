using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cafe_management.Models;
using cafe_management.Models.Authentication;
using X.PagedList;

namespace cafe_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Customers")]
    public class CustomersController : Controller
    {
        private readonly CafeDBContext _context;

        public CustomersController(CafeDBContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = _context.TbCustomers.AsNoTracking().OrderBy(x => x.PhoneNumber).ToList();
            PagedList<TbCustomer> pagedListItem = new PagedList<TbCustomer>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        [Route("Search")]
        [Authentication]
        [HttpGet]
        public IActionResult Search(int? page, string search)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            if (string.IsNullOrEmpty(search))
            {
                return RedirectToAction("Index");
            }

            search = search.ToLower();
            ViewBag.search = search;

            var listItem = _context.TbCustomers.AsNoTracking().Where(x => x.Name.ToLower().Contains(search)).OrderBy(x => x.PhoneNumber).ToList();
            PagedList<TbCustomer> pagedListItem = new PagedList<TbCustomer>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        [Route("Create")]
        [Authentication]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbCustomer khachHang)
        {
            if (ModelState.IsValid)
            {
                // Nếu Id chưa có thì tạo mới (dù constructor đã làm rồi, thêm cho chắc)
                if (khachHang.Id == Guid.Empty)
                {
                    khachHang.Id = Guid.NewGuid();
                }

                _context.TbCustomers.Add(khachHang);
                _context.SaveChanges();

                TempData["Message"] = "Thêm thành công";
                return RedirectToAction("Index", "Customers");
            }
            return View(khachHang);

            //_context.TbCustomers.Add(khachHang);
            //_context.SaveChanges();

            //TempData["Message"] = "Thêm thành công";

            //return RedirectToAction("Index", "Customers");
        }

        //[Route("Edit")]
        //[Authentication]
        //[HttpGet]
        //public IActionResult Edit(int id, string name)
        //{
        //    var khachHang = _context.TbCustomers.Find(id);
        //    ViewBag.name = name;

        //    return View(khachHang);
        //}
        [Route("Edit")]
        [Authentication]
        [HttpGet]
        // SỬA: int id -> Guid? id
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var khachHang = _context.TbCustomers.Find(id);
            if (khachHang == null) return NotFound();

            return View(khachHang);
        }


        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbCustomer khachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(khachHang).State = EntityState.Modified;
                _context.SaveChanges();

                TempData["Message"] = "Sửa thành công";
                return RedirectToAction("Index", "Customers");
            }
            return View(khachHang);
            //_context.Entry(khachHang).State = EntityState.Modified;
            //_context.SaveChanges();

            //TempData["Message"] = "Sửa thành công";

            //return RedirectToAction("Index", "Customers");
        }

        [Route("Delete")]
        [Authentication]
        [HttpGet]
        // SỬA: string id -> Guid? id
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();

            // 1. Kiểm tra xem khách hàng này có hóa đơn nào không?
            // Lưu ý: Giả sử trong TbBill bạn có trường CustomerId là khóa ngoại trỏ về khách hàng
            // Nếu tên trường khóa ngoại trong TbBill khác (vd: MaKhachHang), bạn hãy sửa chữ CustomerId bên dưới.
            var hasBills = _context.TbBills.Any(x => x.CustomerId == id);

            if (hasBills)
            {
                TempData["Message"] = "Không thể xóa: Khách hàng này đang có hóa đơn.";
                return RedirectToAction("Index", "Customers");
            }

            // 2. Tìm và xóa
            var khachHang = _context.TbCustomers.Find(id);
            if (khachHang == null)
            {
                TempData["Message"] = "Không tìm thấy khách hàng";
                return RedirectToAction("Index");
            }

            _context.TbCustomers.Remove(khachHang);
            _context.SaveChanges();

            TempData["Message"] = "Xoá thành công";
            return RedirectToAction("Index", "Customers");
        }

        //[Route("Delete")]
        //[Authentication]
        //[HttpGet]
        //public IActionResult Delete(string id)
        //{
        //    TempData["Message"] = "";

        //    var hoaDon = _context.TbBills.Where(x => x.Id == Guid.Parse(id)).ToList();

        //    if (hoaDon.Count() > 0)
        //    {
        //        TempData["Message"] = "Xoá không thành công";
        //        return RedirectToAction("Index", "Customers");
        //    }

        //    _context.Remove(_context.TbCustomers.Find(id));
        //    _context.SaveChanges();

        //    TempData["Message"] = "Xoá thành công";

        //    return RedirectToAction("Index", "Customers");
        //}
    }
}
