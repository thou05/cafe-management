using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cafe_management.Models
{
    public class TbBill
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Order number is required.")]
        [StringLength(50)]
        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; } = null!; 

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; } 

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")] 
        [Range(typeof(decimal), "0.00", "1000000000.00", ErrorMessage = "Total amount must be zero or positive.")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; } 

        [Required]
        [Display(Name = "Customer")]
        public Guid CustomerId { get; set; } 

        [ForeignKey("CustomerId")]
        public virtual TbCustomer Customer { get; set; } = null!; 
        
        public virtual ICollection<TbBillDetail> SalesOrderDetails { get; set; } = new List<TbBillDetail>();

        // Constructor: Tự động gán Guid và Ngày đặt hàng
        public TbBill()
        {
            Id = Guid.NewGuid();
            OrderDate = DateTime.Now; 
        }
    }
}
