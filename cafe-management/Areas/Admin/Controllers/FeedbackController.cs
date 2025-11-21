//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using cafe_management.Models;
//using cafe_management.Models.Authentication;
//using X.PagedList;

//namespace cafe_management.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    [Route("Admin/Feedback")]
//    public class FeedbackController : Controller
//    {
//        private readonly CafeDBContext _context;

//        public FeedbackController(CafeDBContext context)
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
//            var listItem = _context.TbFeedbacks.AsNoTracking().OrderBy(x => x.Id).ToList();
//            PagedList<TbFeedback> pagedListItem = new PagedList<TbFeedback>(listItem, pageNumber, pageSize);

//            return View(pagedListItem);
//        }

//        [Route("Search")]
//        [Authentication]
//        [HttpGet]
//        public IActionResult Search(int? page, string search)
//        {
//            int pageSize = 30;
//            int pageNumber = page == null || page < 0 ? 1 : page.Value;

//            search = search.ToLower();
//            ViewBag.search = search;

//            var listItem = _context.TbFeedbacks.AsNoTracking().Where(x => x.Title.ToLower().Contains(search)).OrderBy(x => x.Id).ToList();
//            PagedList<TbFeedback> pagedListItem = new PagedList<TbFeedback>(listItem, pageNumber, pageSize);

//            return View(pagedListItem);
//        }

//        [Route("Details")]
//        [Authentication]
//        [HttpGet]
//        public IActionResult Details(int id, string name)
//        {
//            var phanHoi = _context.TbFeedbacks.SingleOrDefault(x => x.Id == id);
//            ViewBag.name = name;

//            return View(phanHoi);
//        }

//        [Route("Delete")]
//        [Authentication]
//        [HttpGet]
//        public IActionResult Delete(int id)
//        {
//            TempData["Message"] = "";

//            _context.Remove(_context.TbFeedbacks.Find(id));
//            _context.SaveChanges();

//            TempData["Message"] = "Xoá thành công";

//            return RedirectToAction("Index", "Fee_contextack");
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cafe_management.Models;
using cafe_management.Models.Authentication;
using X.PagedList;

namespace cafe_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Feedback")]
    public class FeedbackController : Controller
    {
        private readonly CafeDBContext _context;

        public FeedbackController(CafeDBContext context)
        {
            _context = context;
        }

        // Gộp Search vào Index
        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index(int? page, string search)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            // Tạo query cơ bản
            var query = _context.TbFeedbacks.AsNoTracking().AsQueryable();

            // Nếu có từ khóa tìm kiếm thì lọc
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                ViewBag.search = search; // Lưu lại để hiện trên khung tìm kiếm
                query = query.Where(x => x.Title.ToLower().Contains(search));
            }

            // Sắp xếp: ID giảm dần để xem phản hồi mới nhất trước
            var listItem = query.OrderByDescending(x => x.Id).ToList();

            PagedList<TbFeedback> pagedListItem = new PagedList<TbFeedback>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        // Đã xóa Action Search riêng lẻ vì đã gộp vào Index

        [Route("Details")]
        [Authentication]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var phanHoi = _context.TbFeedbacks.SingleOrDefault(x => x.Id == id);

            if (phanHoi == null) return NotFound();

            return View(phanHoi);
        }

        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var phanHoi = _context.TbFeedbacks.Find(id);

            if (phanHoi == null)
            {
                TempData["Message"] = "Không tìm thấy phản hồi";
                return RedirectToAction("Index");
            }

            _context.TbFeedbacks.Remove(phanHoi);
            _context.SaveChanges();

            TempData["Message"] = "Xoá thành công";

            // Đã sửa lỗi chính tả "Fee_contextack" thành "Feedback"
            // Tuy nhiên vì đang ở trong FeedbackController rồi nên chỉ cần ghi "Index" là đủ
            return RedirectToAction("Index");
        }
    }
}
