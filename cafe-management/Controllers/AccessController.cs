using cafe_management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
//using cafe_management.Data;
using cafe_management.Models;

namespace cafe_management.Controllers
{
    public class AccessController : Controller
    {
        private readonly CafeDBContext _context;

        public AccessController(CafeDBContext context)
        {
            _context = context;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the SHA-256 hash of the password bytes
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        // Trong AccessController.cs (bên trong class AccessController)

        // Action Tạm thời để lấy giá trị băm
        //[HttpGet]
        //public IActionResult GetHashedPassword()
        //{
        //    // 1. Đặt mật khẩu gốc bạn muốn dùng (Ví dụ: Caphe2025)
        //    string originalPassword = "12345";

        //    // 2. Gọi hàm băm của bạn
        //    string hashedPassword = HashPassword(originalPassword);

        //    // 3. Trả về chuỗi băm để bạn có thể sao chép
        //    return Content(hashedPassword);
        //}

        // ... các Action Login và Logout vẫn giữ nguyên ...
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "admin");
            }
        }

        [HttpPost]
        public IActionResult Login(TbAccount user)
        {
            // Băm (hash) mật khẩu người dùng nhập vào
            string hashPassword = HashPassword(user.Password);

            // Kiểm tra session trước khi đăng nhập
            if (HttpContext.Session.GetString("UserName") == null)
            {
                // Truy vấn CSDL để tìm tài khoản khớp với Tên đăng nhập VÀ Mật khẩu đã băm
                var u = _context.TbAccounts.Where(x =>
                    x.UserName.Equals(user.UserName) &&
                    x.Password.Equals(hashPassword)
                ).FirstOrDefault();

                if (u != null)
                {
                    // Đăng nhập thành công
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    return RedirectToAction("Index", "admin");
                }
                else
                {
                    // Đăng nhập thất bại: Gán thông báo lỗi vào ViewData
                    ViewData["ErrorMessage"] = "Đăng nhập không thành công. Vui lòng kiểm tra lại Tên đăng nhập hoặc Mật khẩu.";
                }
            }

            // Trả về View (khi đó View sẽ kiểm tra ViewData["ErrorMessage"] để hiển thị)
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");

            return RedirectToAction("Login", "Access");
        }
    }
}
