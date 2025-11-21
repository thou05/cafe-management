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
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = _context.TbBills.AsNoTracking().OrderByDescending(x => x.OrderDate).ToList();
            PagedList<TbBill> pagedListItem = new PagedList<TbBill>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        [Route("Search")]
        [Authentication]
        [HttpGet]
        public IActionResult Search(int? page, string search)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            ViewBag.search = search;

            var listItem = _context.TbBills.AsNoTracking().Where(x => x.OrderDate.ToString().Contains(search)).OrderBy(x => x.Id).ToList();
            PagedList<TbBill> pagedListItem = new PagedList<TbBill>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        [Route("Details")]
        [Authentication]
        [HttpGet]
        public IActionResult Details(int? page, string id, string name)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = _context.TbBillDetails.AsNoTracking().Where(x => x.BillId == Guid.Parse(id)).OrderBy(x => x.BillId).ToList();
            PagedList<TbBillDetail> pagedListItem = new PagedList<TbBillDetail>(listItem, pageNumber, pageSize);

            ViewBag.name = name;

            return View(pagedListItem);
        }
    }
}
