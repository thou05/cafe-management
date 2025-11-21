using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cafe_management.Models;
using cafe_management.Models.Authentication;
using X.PagedList;

namespace cafe_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Bill")]
    public class BillController : Controller
    {
        private readonly CafeDBContext _context;

        public BillController(CafeDBContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index(int? page, string search)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            // Khởi tạo query
            var query = _context.TbBills.AsNoTracking().AsQueryable();

            // Xử lý tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                // Lưu từ khóa để hiện lại trên View
                ViewBag.search = search;

                // Tìm kiếm theo ngày tháng (hoặc CustomerId nếu bạn muốn mở rộng)
                // Lưu ý: ToString() trong LINQ đôi khi gây chậm, nhưng giữ nguyên logic cũ của bạn
                query = query.Where(x => x.OrderDate.ToString().Contains(search));
            }

            // Sắp xếp: Luôn giảm dần theo ngày để thấy hóa đơn mới nhất
            var listItem = query.OrderByDescending(x => x.OrderDate).ToList();

            PagedList<TbBill> pagedListItem = new PagedList<TbBill>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        // Đã xóa Action Search vì gộp vào Index

        [Route("Details")]
        [Authentication]
        [HttpGet]
        // SỬA: Đổi string id -> Guid id để tránh lỗi parse
        public IActionResult Details(int? page, Guid? id)
        {
            if (id == null) return NotFound();

            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            // Lấy chi tiết hóa đơn theo BillId
            var listItem = _context.TbBillDetails
                                   .AsNoTracking()
                                   .Where(x => x.BillId == id)
                                   .OrderBy(x => x.BillId) // Hoặc order by ProductId
                                   .ToList();

            PagedList<TbBillDetail> pagedListItem = new PagedList<TbBillDetail>(listItem, pageNumber, pageSize);

            // Lấy thêm thông tin Hóa đơn cha để hiển thị tên khách/ngày giờ trên tiêu đề nếu cần
            var bill = _context.TbBills.Find(id);
            if (bill != null)
            {
                // Giả sử bạn muốn hiển thị mã hóa đơn hoặc ngày
                ViewBag.BillInfo = bill.OrderDate;
            }

            return View(pagedListItem);
        }
    }
}