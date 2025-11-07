using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cafe_management.Models
{
    public class TbProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //cột tự tăng
        public int Id { get; set; } 

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; } = null!; 

        [Required(ErrorMessage = "Price is required.")]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0.01", "1000000.00", ErrorMessage = "Price must be greater than 0.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(500)]
        [DataType(DataType.ImageUrl)]
        public string? ImageUrl { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; } 

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("ProductCategoryId")]
        public virtual TbCategory CategoryIdNavigation { get; set; } = null!; 

        
        public virtual ICollection<TbBillDetail> BillDetails { get; set; } = new List<TbBillDetail>();
    }
}
