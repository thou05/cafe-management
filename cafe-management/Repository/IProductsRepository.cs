using cafe_management.ViewModels;
namespace cafe_management.Repository
{
    public interface IProductsRepository
    {
        List<ProductViewModel> GetProducts();
        ProductViewModel GetProductById(int id);
        CreateProductViewModel CreateProduct(ProductViewModel product);
        void Update(ProductViewModel product);
        void Delete(int id);
    }
}
