using cafe_management.Models;
using cafe_management.Models.Authentication;
using cafe_management.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace cafe_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class HomeAdminController : Controller
    {
        private readonly CafeDBContext _context;
        //lấy thông tin về môi trường web hiện tại
        IWebHostEnvironment hostEnvironment;


        public HomeAdminController(CafeDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }


        [Route("")]
        [Authentication]

        public IActionResult Index(string search, int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            //  Giữ lại tham số tìm kiếm để truyền cho View (dùng trong Paginator và ô search)
            ViewBag.SearchTerm = search;

            // Bắt đầu câu truy vấn (chưa thực thi)
            var query = (from product in _context.TbProducts
                         join type in _context.TbCategories on product.CategoryId equals type.Id
                         select new ProductViewModel
                         {
                             Id = product.Id,
                             Name = product.Name,
                             Price = product.Price,
                             Description = product.Description,
                             ImageUrl = product.ImageUrl,
                             Notes = product.Notes,
                             Category = type.Name
                         });

            // Nếu có tìm kiếm, thêm điều kiện Where
            if (!String.IsNullOrEmpty(search))
            {
                string lowerSearch = search.ToLower();
                // Lọc trên IQueryable, chứ không phải List
                query = query.Where(p => p.Name.ToLower().Contains(lowerSearch));
            }

            // Sắp xếp và thực thi truy vấn (ToList)
            var listItem = query.OrderBy(p => p.Id).ToList();

            // Phân trang
            PagedList<ProductViewModel> pagedListItem = new PagedList<ProductViewModel>(listItem, pageNumber, pageSize);

            // Luôn trả về view "Index"
            return View(pagedListItem);
        }


        [Route("Create")]
        [HttpGet]
        [Authentication]
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.TbCategories.ToList(), "Id", "Name");

            return View();
        }

        
        //public IActionResult Create(TbProduct product, IFormFile imageFile)
        //{
           
        //    if (imageFile != null && imageFile.Length > 0)
        //    {
        //        // Đối với mục đích minh họa, chúng ta sẽ lưu ảnh vào thư mục Images trong wwwroot
        //        string uploadFolder = Path.Combine(Path.Combine(hostEnvironment.WebRootPath, "img"), "products");
        //        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
        //        string filePath = Path.Combine(uploadFolder, uniqueFileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            imageFile.CopyTo(stream);
        //        }

        //        // Lưu đường dẫn hoặc thông tin về ảnh vào cơ sở dữ liệu nếu cần
        //        // Ví dụ: lưu đường dẫn filePath vào cơ sở dữ liệu
        //        // ...

        //        return RedirectToAction("Index");
        //    }

        //    //db.TbSanPhams.Add(product);
        //    _context.SaveChanges();
        //    TempData["Message"] = "Add successfully";

        //    return RedirectToAction("Index", "HomeAdmin");
        //}

        [Route("Create")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Tham số (TbProduct product, IFormFile imageFile) giờ đã khớp với View
        public IActionResult Create(TbProduct product, IFormFile imageFile)
        {
            // 1. Kiểm tra xem các trường (Name, Price,...) có hợp lệ không

            if (ModelState.IsValid)
            {
                try
                {
                    // 2. Xử lý file ảnh nếu có
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string uploadFolder = Path.Combine(Path.Combine(hostEnvironment.WebRootPath, "img"), "products");

                        // Tạo tên file unique để không bị trùng
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);

                        // Lưu file vào thư mục
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            imageFile.CopyTo(stream);
                        }

                        product.ImageUrl = uniqueFileName;
                    }
                    else
                    {
                        product.ImageUrl = null;
                    }

                    _context.TbProducts.Add(product);
                    _context.SaveChanges();

                    TempData["Message"] = "Add successfully";
                    return RedirectToAction("Index", "HomeAdmin");
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Error adding product.";
                }
            }
            ViewBag.CategoryId = new SelectList(_context.TbCategories.ToList(), "Id", "Name", product.CategoryId);

           
            return View(product);
        }


        [Route("Details")]
        [Authentication]
        [HttpGet]
        public IActionResult Details(int id, string name)
        {
            var productItem = (from product in _context.TbProducts
                               join type in _context.TbCategories on product.CategoryId equals type.Id
                               where product.Id == id
                               select new ProductViewModel
                               {
                                   Id = product.Id,
                                   Name = product.Name,
                                   Price = product.Price,
                                   Description = product.Description,
                                   ImageUrl = product.ImageUrl,
                                   Notes = product.Notes,
                                   Category = type.Name
                               }).SingleOrDefault();

            ViewBag.name = name;

            return View(productItem);
        }

        [Route("Edit")]
        [Authentication]
        [HttpGet]
        public IActionResult Edit(int id, string name)
        {
            var sanPham = _context.TbProducts.Find(id);

            ViewBag.MaNhomSp = new SelectList(_context.TbCategories.ToList(), "Id", "Name");
            ViewBag.name = name;

            return View(sanPham);
        }


        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbProduct product, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy sản phẩm cũ trong DB
                    var oldProduct = _context.TbProducts.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);
                    if (oldProduct == null)
                    {
                        return NotFound();
                    }

                    // Nếu có ảnh mới => upload
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string uploadFolder = Path.Combine(hostEnvironment.WebRootPath, "img", "products");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            imageFile.CopyTo(stream);
                        }

                        product.ImageUrl = uniqueFileName;
                    }
                    else
                    {
                        // Không có ảnh mới => giữ nguyên ảnh cũ
                        product.ImageUrl = oldProduct.ImageUrl;
                    }

                    _context.Entry(product).State = EntityState.Modified;
                    _context.SaveChanges();

                    TempData["Message"] = "Edit successfully";
                    return RedirectToAction("Index", "HomeAdmin");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Lỗi khi cập nhật: {ex.Message}");
                }
            }

            ViewBag.MaNhomSp = new SelectList(_context.TbCategories.ToList(), "Id", "Name", product.CategoryId);
            ViewBag.name = product.Name;
            return View(product);
        }


        [Route("Delete")]
        [HttpGet]
        [Authentication]
        public IActionResult Delete(int id)
        {
            TempData["Message"] = "";
            var chiTietHoaDon = _context.TbBillDetails.Where(x => x.ProductId == id).ToList();

            if (chiTietHoaDon.Count() > 0)
            {
                TempData["Message"] = "Cannot delete product";

                return RedirectToAction("Index", "HomeAdmin");
            }

            _context.Remove(_context.TbProducts.Find(id));
            _context.SaveChanges();

            TempData["Message"] = "Deleted product";

            return RedirectToAction("Index", "HomeAdmin");
        }

    }
}
