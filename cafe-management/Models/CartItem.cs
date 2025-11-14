namespace cafe_management.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Img { get; set; } = null!;
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public decimal? TotalAmount => Quantity * Price;
    }
}
