using Microsoft.EntityFrameworkCore;
using cafe_management.ViewModels;
using cafe_management.Models;

namespace cafe_management.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly CafeDBContext _context;

        public ProductsRepository(CafeDBContext context)
        {
            _context = context;
        }

        public List<ProductViewModel> GetProducts()
        {
            var products = _context.TbProducts.Select(p => new ProductViewModel
            {
                MaSanPham = p.Id,
                TenSanPham = p.Name,
                GiaBan = p.Price,
                MoTa = p.Description,
                HinhAnh = p.ImageUrl,
                GhiChu = p.Notes,
                LoaiSanPham = p.Id.ToString(),
            });

            return products.ToList();
        }

        public CreateProductViewModel CreateProduct(ProductViewModel product)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ProductViewModel GetProductById(int id)
        {
            throw new NotImplementedException();
        }



        public void Update(ProductViewModel product)
        {
            throw new NotImplementedException();
        }
    }
}
