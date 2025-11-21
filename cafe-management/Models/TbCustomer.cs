using System.ComponentModel.DataAnnotations;

namespace cafe_management.Models
{
    public class TbCustomer
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên khách hàng phải từ 3 đến 100 ký tự.")]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(10, ErrorMessage = "Số điện thoại tối đa 10 số.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "SĐT không hợp lệ (Phải bắt đầu bằng số 0 và đủ 10 số).")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, ErrorMessage = "Địa chỉ không được vượt quá 250 ký tự.")]
        public string Address { get; set; } = null!;


        public virtual ICollection<TbBill> Bills { get; set; } = new List<TbBill>();

        // Constructor: Tự động tạo Guid khi tạo mới    
        public TbCustomer()
        {
            Id = Guid.NewGuid();
        }
    }
}
