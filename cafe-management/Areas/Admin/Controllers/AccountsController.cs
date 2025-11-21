using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using cafe_management.Models;
using cafe_management.Models.Authentication;
using X.PagedList;

namespace cafe_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Accounts")]
    public class AccountsController : Controller
    {
        private readonly CafeDBContext _context;

        public AccountsController(CafeDBContext context)
        {
            _context = context;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to a byte array
                byte[] passwor_contextytes = Encoding.UTF8.GetBytes(password);

                // Compute the SHA-256 hash of the password bytes
                byte[] hashBytes = sha256.ComputeHash(passwor_contextytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = _context.TbAccounts.AsNoTracking().OrderBy(x => x.UserName).ToList();
            PagedList<TbAccount> pagedListItem = new PagedList<TbAccount>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        [Route("Create")]
        [Authentication]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[Route("Create")]
        ////[Authentication]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(TbAccount quanTriVien)
        //{
        //    _context.TbAccounts.Add(quanTriVien);
        //    _context.SaveChanges();
        //    TempData["Message"] = "Thêm thành công";

        //    return RedirectToAction("Index", "Accounts");
        //}

        [Route("Create")]
        [HttpPost]
        [Authentication]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbAccount account)
        {
            if (ModelState.IsValid)
            {
                // Hash mật khẩu trước khi lưu vào DB
                // Kiểm tra nếu password null để tránh lỗi
                if (!string.IsNullOrEmpty(account.Password))
                {
                    account.Password = HashPassword(account.Password);
                }

                _context.TbAccounts.Add(account);
                _context.SaveChanges();

                TempData["Message"] = "Thêm thành công";
                return RedirectToAction("Index", "Accounts");
            }

            // Nếu form lỗi thì trả về View để nhập lại
            return View(account);
            //// Hash mật khẩu trước khi lưu vào DB
            //string hashPass = AccountsController.HashPassword(quanTriVien.Password);
            //quanTriVien.Password = hashPass;

            //_context.TbAccounts.Add(quanTriVien);
            //_context.SaveChanges();

            //TempData["Message"] = "Thêm thành công";
            //return RedirectToAction("Index", "Accounts");
        }

        //[Route("Edit")]
        //[Authentication]
        //[HttpGet]
        //public IActionResult Edit(int id, string name)
        //{
        //    var quanTriVien = _context.TbAccounts.Find(id);
        //    ViewBag.id = id;

        //    return View(quanTriVien);
        //}

        [Route("Edit")]
        [Authentication]
        [HttpGet]
        // QUAN TRỌNG: Đổi int id thành Guid id
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var account = _context.TbAccounts.Find(id);
            if (account == null) return NotFound();
            return View(account);

            //var quanTriVien = _context.TbAccounts.Find(id);
            //if (quanTriVien == null) return NotFound();

            //ViewBag.id = id;
            //return View(quanTriVien);
        }

        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbAccount account)
        {
            var existingAccount = _context.TbAccounts.Find(account.Id);

            if (existingAccount != null)
            {
                // Cập nhật UserName
                existingAccount.UserName = account.UserName;

                // Logic Password:
                // Nếu người dùng nhập pass mới thì hash và lưu.
                // Nếu người dùng để trống hoặc nhập lại pass cũ (đã hash) thì bỏ qua.
                if (!string.IsNullOrEmpty(account.Password) && account.Password != existingAccount.Password)
                {
                    // Chỉ hash nếu password thay đổi
                    existingAccount.Password = HashPassword(account.Password);
                }

                _context.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }

            return View(account);

            //string hashPass = HashPassword(quanTriVien.Password);

            //quanTriVien.Password = hashPass;

            //_context.Entry(quanTriVien).State = EntityState.Modified;
            //_context.SaveChanges();

            //TempData["Message"] = "Sửa thành công";

            //return RedirectToAction("Index", "Accounts");
        }


        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(Guid? id) // Dùng Guid? id
        {
            if (id == null) return NotFound();

            var account = _context.TbAccounts.Find(id);

            if (account == null)
            {
                TempData["Message"] = "Không tìm thấy tài khoản";
                return RedirectToAction("Index");
            }

            _context.TbAccounts.Remove(account);
            _context.SaveChanges();

            TempData["Message"] = "Xoá thành công";
            return RedirectToAction("Index");
        }

        //[Route("Delete")]
        //[Authentication]
        //[HttpGet]
        //public IActionResult Delete(string id)
        //{
        //    TempData["Message"] = "";

        //    _context.Remove(_context.TbAccounts.Find(id));
        //    _context.SaveChanges();

        //    TempData["Message"] = "Xoá thành công";

        //    return RedirectToAction("Index", "Accounts");
        //}
    }
}
