using cafe_management.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

namespace cafe_management.Controllers
{
    public class NewsController : Controller
    {
        private readonly CafeDBContext _context;

        public NewsController(CafeDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page)
        {   
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = _context.TbNews.AsNoTracking().OrderByDescending(x => x.PostedDate).ToList();
            PagedList<TbNews> pagedListItem = new PagedList<TbNews>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        public IActionResult Details(int? id)
        {
            var newPost = _context.TbNews.SingleOrDefault(x => x.Id == id);

            return View(newPost);
        }
    }
}