using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cafe_management.Models
{
    public class TbBillDetail
    {
        [Required]
        public Guid BillId { get; set; } 

        [Required]
        public int ProductId { get; set; }

       
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")] 
        [Range(typeof(decimal), "0.01", "1000000.00", ErrorMessage = "Price must be greater than 0.")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; } 

        [Required]
        [Range(1, 1000, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; } 

        [Required] 
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(typeof(decimal), "0.00", "1000000.00", ErrorMessage = "Discount must be positive.")]
        public decimal Discount { get; set; } = 0; 

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(typeof(decimal), "0.01", "1000000000.00")]
        [Display(Name = "Total")]
        public decimal Total { get; set; } 

      
        [ForeignKey("BillId")]
        public virtual TbBill Bill { get; set; } = null!; 

        [ForeignKey("ProductId")]
        public virtual TbProduct Product { get; set; } = null!;
    }
}
