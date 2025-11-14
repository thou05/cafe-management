using System.ComponentModel.DataAnnotations;

namespace cafe_management.Models
{
    public class TbCustomer
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(15)]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250)]
        public string Address { get; set; } = null!;


        public virtual ICollection<TbBill> Bills { get; set; } = new List<TbBill>();

        // Constructor: Tự động tạo Guid khi tạo mới    
        public TbCustomer()
        {
            Id = Guid.NewGuid();
        }
    }
}
