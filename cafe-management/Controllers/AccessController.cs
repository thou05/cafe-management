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
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    return RedirectToAction("Index", "admin");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Đăng nhập không thành công. Vui lòng kiểm tra lại Tên đăng nhập hoặc Mật khẩu.";
                }
            }

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
