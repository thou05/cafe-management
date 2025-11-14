using cafe_management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace cafe_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/news")]
    public class NewsManageController : Controller
    {
        private readonly CafeDBContext _context;

        public NewsManageController(CafeDBContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = _context.TbNews.AsNoTracking().OrderBy(x => x.Id).ToList();
            PagedList<TbNews> pagedListItem = new PagedList<TbNews>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }


        [Route("Search")]
        [HttpGet]
        public IActionResult Search(int? page, string search)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            search = search.ToLower();
            ViewBag.search = search;

            var listItem = _context.TbNews.AsNoTracking().Where(x => x.Title.ToLower().Contains(search)).OrderBy(x => x.Id).ToList();
            PagedList<TbNews> pagedListItem = new PagedList<TbNews>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.NguoiDang = new SelectList(_context.TbAccounts.ToList(), "UserName", "UserName");

            return View();
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbNews news)
        {
            _context.TbNews.Add(news);
            _context.SaveChanges();

            TempData["Message"] = "Add successfully";

            return RedirectToAction("Index", "NewsManage");
        }

        [Route("Details")]
        [HttpGet]
        public IActionResult Details(int id, string name)
        {
            var news = _context.TbNews.SingleOrDefault(x => x.Id == id);
            ViewBag.name = name;

            return View(news);
        }

        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(int id, string name)
        {
            var news = _context.TbNews.Find(id);

            ViewBag.UserPost = new SelectList(_context.TbAccounts.ToList(), "UserName", "UserName");
            ViewBag.name = name;

            return View(news);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbNews news)
        {
            _context.Entry(news).State = EntityState.Modified;
            _context.SaveChanges();

            TempData["Message"] = "Edit successfully";

            return RedirectToAction("Index", "NewsManage");
        }

        [Route("Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            TempData["Message"] = "";

            _context.Remove(_context.TbNews.Find(id));
            _context.SaveChanges();

            TempData["Message"] = "Delete successfully";

            return RedirectToAction("Index", "NewsManage");
        }


    }
}
