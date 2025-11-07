using System.ComponentModel.DataAnnotations;

namespace cafe_management.Models
{
    public class TbAccount
    {
        [Key] 
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "Password must be at least 3 characters long.")]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        public TbAccount()
        {
            Id = Guid.NewGuid();
        }
    }
}
