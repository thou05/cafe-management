namespace cafe_management.ViewModels
{
    public class CreateProductViewModel
    {
       
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public IFormFile ImageUrl { get; set; } = null!;
       
        public string? Notes { get; set; }

        public int CategoryId { get; set; } 
    }
}
