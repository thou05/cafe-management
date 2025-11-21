//using cafe_management.Models;
//using cafe_management.Models.Authentication;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using X.PagedList;

//namespace cafe_management.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    [Route("admin/news")]
//    public class NewsManageController : Controller
//    {
//        private readonly CafeDBContext _context;

//        public NewsManageController(CafeDBContext context)
//        {
//            _context = context;
//        }

//        [Route("")]
//        [Route("index")]
//        [Authentication]
//        public IActionResult Index(int? page)
//        {
//            int pageSize = 30;
//            int pageNumber = page == null || page < 0 ? 1 : page.Value;
//            var listItem = _context.TbNews.AsNoTracking().OrderBy(x => x.Id).ToList();
//            PagedList<TbNews> pagedListItem = new PagedList<TbNews>(listItem, pageNumber, pageSize);

//            return View(pagedListItem);
//        }


//        [Route("Search")]
//        [HttpGet]
//        [Authentication]
//        public IActionResult Search(int? page, string search)
//        {
//            int pageSize = 30;
//            int pageNumber = page == null || page < 0 ? 1 : page.Value;

//            search = search.ToLower();
//            ViewBag.search = search;

//            var listItem = _context.TbNews.AsNoTracking().Where(x => x.Title.ToLower().Contains(search)).OrderBy(x => x.Id).ToList();
//            PagedList<TbNews> pagedListItem = new PagedList<TbNews>(listItem, pageNumber, pageSize);

//            return View(pagedListItem);
//        }

//        [Route("Create")]
//        [HttpGet]
//        [Authentication]
//        public IActionResult Create()
//        {
//            ViewBag.NguoiDang = new SelectList(_context.TbAccounts.ToList(), "UserName", "UserName");

//            return View();
//        }

//        [Route("Create")]
//        [HttpPost]
//        [Authentication]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(TbNews news)
//        {
//            _context.TbNews.Add(news);
//            _context.SaveChanges();

//            TempData["Message"] = "Add successfully";

//            return RedirectToAction("Index", "NewsManage");
//        }

//        [Route("Details")]
//        [HttpGet]
//        [Authentication]
//        public IActionResult Details(int id, string name)
//        {
//            var news = _context.TbNews.SingleOrDefault(x => x.Id == id);
//            ViewBag.name = name;

//            return View(news);
//        }

//        [Route("Edit")]
//        [HttpGet]
//        [Authentication]
//        public IActionResult Edit(int id, string name)
//        {
//            var news = _context.TbNews.Find(id);

//            ViewBag.UserPost = new SelectList(_context.TbAccounts.ToList(), "UserName", "UserName");
//            ViewBag.name = name;

//            return View(news);
//        }

//        [Route("Edit")]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authentication]
//        public IActionResult Edit(TbNews news)
//        {
//            _context.Entry(news).State = EntityState.Modified;
//            _context.SaveChanges();

//            TempData["Message"] = "Edit successfully";

//            return RedirectToAction("Index", "NewsManage");
//        }

//        [Route("Delete")]
//        [HttpGet]
//        [Authentication]
//        public IActionResult Delete(int id)
//        {
//            TempData["Message"] = "";

//            _context.Remove(_context.TbNews.Find(id));
//            _context.SaveChanges();

//            TempData["Message"] = "Delete successfully";

//            return RedirectToAction("Index", "NewsManage");
//        }


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
    [Route("admin/news")]
    public class NewsManageController : Controller
    {
        private readonly CafeDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment; // Cần cái này để lưu ảnh

        public NewsManageController(CafeDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // --- 1. INDEX & SEARCH GỘP LÀM 1 ---
        [Route("")]
        [Route("index")]
        [Authentication]
        public IActionResult Index(string search, int? page)
        {
            int pageSize = 10; // Tin tức nên để ít hơn (vd: 10) cho dễ nhìn
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            // Giữ lại từ khóa tìm kiếm
            ViewBag.SearchTerm = search;

            // Tạo query
            var query = _context.TbNews.AsNoTracking().AsQueryable();

            // Lọc dữ liệu nếu có tìm kiếm
            if (!String.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Title.ToLower().Contains(search.ToLower()));
            }

            // Sắp xếp: Tin mới nhất lên đầu (OrderByDescending)
            var pagedList = query.OrderByDescending(x => x.PostedDate).ToPagedList(pageNumber, pageSize);

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
        public IActionResult Create(TbNews news, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Xử lý upload ảnh
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "img", "news");
                        // Tạo thư mục nếu chưa có
                        if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            imageFile.CopyTo(stream);
                        }
                        news.Image = uniqueFileName;
                    }

                    // Nếu không chọn ngày, mặc định lấy ngày hiện tại
                    if (news.PostedDate == null)
                    {
                        news.PostedDate = DateTime.Now;
                    }

                    _context.TbNews.Add(news);
                    _context.SaveChanges();

                    TempData["MessageType"] = "success";
                    TempData["Message"] = "Thêm tin tức thành công";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi hệ thống: " + ex.Message);
                }
            }
            return View(news);
        }

        // --- 3. DETAILS ---
        [Route("Details")]
        [HttpGet]
        [Authentication]
        public IActionResult Details(int id)
        {
            var news = _context.TbNews.SingleOrDefault(x => x.Id == id);
            if (news == null) return NotFound();

            return View(news);
        }

        // --- 4. EDIT ---
        [Route("Edit")]
        [HttpGet]
        [Authentication]
        public IActionResult Edit(int id)
        {
            var news = _context.TbNews.Find(id);
            if (news == null) return NotFound();

            return View(news);
        }

        [Route("Edit")]
        [HttpPost]
        [Authentication]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbNews news, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy tin cũ để check ảnh
                    var oldNews = _context.TbNews.AsNoTracking().FirstOrDefault(x => x.Id == news.Id);

                    if (oldNews != null)
                    {
                        if (imageFile != null && imageFile.Length > 0)
                        {
                            // Có chọn ảnh mới -> Upload ảnh mới
                            string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "img", "news");
                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                            string filePath = Path.Combine(uploadFolder, uniqueFileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                imageFile.CopyTo(stream);
                            }
                            news.Image = uniqueFileName;
                        }
                        else
                        {
                            // Không chọn ảnh mới -> Giữ nguyên ảnh cũ
                            news.Image = oldNews.Image;
                        }

                        _context.Entry(news).State = EntityState.Modified;
                        _context.SaveChanges();

                        TempData["MessageType"] = "success";
                        TempData["Message"] = "Cập nhật bài viết thành công";

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi cập nhật: " + ex.Message);
                }
            }
            return View(news);
        }

        // --- 5. DELETE ---
        [Route("Delete")]
        [HttpGet]
        [Authentication]
        public IActionResult Delete(int id)
        {
            var news = _context.TbNews.Find(id);
            if (news == null)
            {
                TempData["MessageType"] = "danger";
                TempData["Message"] = "Không tìm thấy bài viết.";
                return RedirectToAction("Index");
            }

            try
            {
                _context.TbNews.Remove(news);
                _context.SaveChanges();

                TempData["MessageType"] = "success";
                TempData["Message"] = "Xóa bài viết thành công";
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