using cafe_management.Helpers;
using cafe_management.Models;
using Microsoft.AspNetCore.Mvc;

namespace cafe_management.Controllers
{
    public class CartController : Controller
    {
        private readonly CafeDBContext _context;

        public CartController(CafeDBContext context)
        {
            _context = context;
        }

        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("Cart");

                if (data == null)
                {
                    data = new List<CartItem>();
                }

                return data;
            }
        }

        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");

            if (cartItems != null && cartItems.Any())
            {
                decimal? tongTien = cartItems.Sum(p => p.Price * p.Quantity);
                string totalCart = tongTien.Value.ToString("n0");
                ViewData["total"] = totalCart;
                return View(Carts);
            }
            else
            {
                ViewData["Message"] = "Không có sản phẩm trong giỏ hàng.";
                ViewData["total"] = "0";
                return View(Carts);
            }
        }

        public IActionResult Add(int id, int quantity)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.Id == id);
            decimal? tongTien = 0;

            if (item == null)
            {
                var hangHoa = _context.TbProducts.SingleOrDefault(p => p.Id == id);

                item = new CartItem
                {
                    Id = id,
                    Name = hangHoa.Name,
                    Price = hangHoa.Price,
                    Quantity = quantity,
                    Img = hangHoa.ImageUrl
                };

                myCart.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }



            HttpContext.Session.Set("Cart", myCart);
            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Update([FromBody] List<UpdateQuantityRequest> updates)
        {
            if (updates == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid request.");
            }

            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");

            if (cartItems != null)
            {
                foreach (var update in updates)
                {
                    var cartItemToUpdate = cartItems.Find(item => item.Id == update.ProductId);

                    if (cartItemToUpdate != null)
                    {
                        cartItemToUpdate.Quantity = update.Quantity;
                    }
                }

                HttpContext.Session.Set("Cart", cartItems);

                decimal? totalAmount = 0;
                foreach (var item in cartItems)
                {
                    totalAmount += item.Quantity * item.Price;
                }

                return Json(new { success = true, message = "Số lượng đã được cập nhật.", totalAmount = totalAmount, cartItems = cartItems });
            }

            return BadRequest("Invalid cart.");
        }

        public class UpdateQuantityRequest
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        [HttpPost]
        public IActionResult Remove(int maSp)
        {
            try
            {
                var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");

                if (cartItems != null)
                {
                    var productToRemove = cartItems.SingleOrDefault(item => item.Id == maSp);

                    if (productToRemove != null)
                    {
                        cartItems.Remove(productToRemove);

                        HttpContext.Session.Set("Cart", cartItems);

                        decimal? totalAmount = 0;

                        foreach (var item in cartItems)
                        {
                            totalAmount += item.Quantity * item.Price;
                        }

                        return Json(new { success = true, message = "Đã xoá sản phẩm.", totalAmount = totalAmount, cartItems = cartItems });
                    }
                    else
                    {
                        Console.WriteLine(maSp);
                        return Json(new { success = false, message = "Không có sản phẩm." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Giỏ hàng rỗng." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Checkout()
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");


            if (cartItems != null && cartItems.Any())
            {
                decimal? tongTien = cartItems.Sum(p => p.Price * p.Quantity);
                string totalCart = tongTien.Value.ToString("n0");
                ViewData["total"] = totalCart;
                return View(Carts);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        public IActionResult Confirmation(string customerName, string phoneNumber, string address)
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");

            Random random = new Random();
            Guid orderId = Guid.NewGuid();
            Guid customerId = Guid.NewGuid();

            if (cartItems != null && cartItems.Any())
            {
                var custormer = _context.TbCustomers.FirstOrDefault(x => x.PhoneNumber.Equals(phoneNumber));

                if (custormer == null)
                {
                    var _customer = new TbCustomer
                    {
                        Id = customerId,
                        Name = customerName,
                        PhoneNumber = phoneNumber,
                        Address = address
                    };

                    _context.TbCustomers.Add(_customer);
                    _context.SaveChanges();

                    var order = new TbBill
                    {
                        Id = orderId,
                        OrderNumber = random.Next(1, 100).ToString(),
                        OrderDate = DateTime.Now,
                        TotalAmount = cartItems.Sum(p => (p.Price ?? 0) * p.Quantity),
                        CustomerId = customerId
                    };

                    _context.TbBills.Add(order);
                    _context.SaveChanges();
                }
                else
                {
                    custormer.Name = customerName;
                    custormer.Address = address;
                    _context.SaveChanges();

                    var order = new TbBill
                    {
                        Id = orderId,
                        OrderNumber = random.Next(1, 100).ToString(),
                        OrderDate = DateTime.Now,
                        TotalAmount = cartItems.Sum(p => (p.Price ?? 0)* p.Quantity),
                        CustomerId = custormer.Id
                    };

                    _context.TbBills.Add(order);
                    _context.SaveChanges();
                }

                foreach (var cartItem in cartItems)
                {
                    var orderDetails = new TbBillDetail
                    {
                        BillId = orderId,
                        ProductId = cartItem.Id,
                        UnitPrice = cartItem.Price ?? 0,
                        Discount = 0,
                        Quantity = cartItem.Quantity,
                        Total = cartItem.TotalAmount ?? 0
                    };

                    _context.TbBillDetails.Add(orderDetails);
                    _context.SaveChanges();
                }

                HttpContext.Session.Remove("Cart");

                return RedirectToAction("success");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
